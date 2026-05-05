namespace Ecommerce.Api.Infrastructure.Repositories.CartRepository;

using System.Transactions;
using DefaultNamespace;
using Domain.Cart;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Abstractions;

public class CartRepository : ICartRepository
{
    private readonly Db _databaseConnection;

    public CartRepository(Db databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task<AddCartItemResponse> AddToCart(int userId, int productId, int quantity)
    {
        using var connection = _databaseConnection.CreateConnection();
        await connection.OpenAsync();

        // verificam daca userul are cart
        int cartId;
        var getCartCmd = new SqlCommand(
            @"SELECT Id FROM Carts WHERE UserId = @UserId", connection);

        getCartCmd.Parameters.AddWithValue("@UserId", userId);

        var result = await getCartCmd.ExecuteScalarAsync();

        // daca nu are cart cream => cart
        if (result == null)
        {
            var createCartCmd = new SqlCommand(
                @"INSERT INTO Carts (UserId) OUTPUT INSERTED.Id VALUES (@UserId)", connection);
            createCartCmd.Parameters.AddWithValue("@UserId", userId);
            cartId = (int)await createCartCmd.ExecuteScalarAsync();
        }
        else
        {
            cartId = (int)result;
        }

        // verificam daca produsul exista deja in cart
        var checkCartItemCmd = new SqlCommand(
            @"SELECT Id, Quantity FROM CartItems WHERE CartId=@CartId AND ProductId=@ProductId", connection);
        checkCartItemCmd.Parameters.AddWithValue("@CartId", cartId);
        checkCartItemCmd.Parameters.AddWithValue("@ProductId", productId);

        using var reader = await checkCartItemCmd.ExecuteReaderAsync();

        // daca exista => marim Quantity
        if (await reader.ReadAsync())
        {
            int cartItemId = reader.GetInt32(0);
            int existingQty = reader.GetInt32(1);

            reader.Close();
            var updateCartItemCmd = new SqlCommand(
                @"UPDATE CartItems SET Quantity=@Quantity WHERE Id = @Id", connection);
            updateCartItemCmd.Parameters.AddWithValue("@Quantity", existingQty + quantity);
            updateCartItemCmd.Parameters.AddWithValue("@Id", cartItemId);

            await updateCartItemCmd.ExecuteNonQueryAsync();
        }
        else
        {
            reader.Close();
            // daca nu => inseram item nou
            var insertNewItem = new SqlCommand(
                @"INSERT INTO CartItems (CartId, ProductId, Quantity) VALUES (@CartId, @ProductId, @Quantity)",
                connection);
            insertNewItem.Parameters.AddWithValue("@CartId", cartId);
            insertNewItem.Parameters.AddWithValue("@ProductId", productId);
            insertNewItem.Parameters.AddWithValue("@Quantity", quantity);

            await insertNewItem.ExecuteNonQueryAsync();
        }

        // return CartItemDto
        var selectCartItemCmd = new SqlCommand(
            @"
                SELECT 
                    ci.Id as CartItemId,
                    p.Id as ProductId,
                    p.ImageUrl,
                    p.Description,
                    p.Price,
                    ci.Quantity,
                    (p.Price * ci.Quantity) as Total
                FROM CartItems ci
                JOIN Products p
                    ON ci.ProductId = p.Id
                WHERE ci.CartId = @CartId
                AND ci.ProductId = @ProductId
                ", connection);
        selectCartItemCmd.Parameters.AddWithValue("@CartId", cartId);
        selectCartItemCmd.Parameters.AddWithValue("@ProductId", productId);

        using var readerItem = await selectCartItemCmd.ExecuteReaderAsync();

        AddCartItemResponse item = null;

        if (await readerItem.ReadAsync())
        {
            item = new AddCartItemResponse
            {
                Id = (int)readerItem["CartItemId"],
                ProductId = (int)readerItem["ProductId"],
                ImageUrl = readerItem["ImageUrl"].ToString(),
                Description = readerItem["Description"].ToString(),
                Price = (decimal)readerItem["Price"],
                Quantity = (int)readerItem["Quantity"],
                Total = (decimal)readerItem["Total"]
            };
        }

        return item;
    }

    public async Task<List<CartItemDto>> GetCart(int userId)
    {
        using var connection = _databaseConnection.CreateConnection();
        await connection.OpenAsync();

        // luam cartId
        var getCartCmd = new SqlCommand(@"
        SELECT Id FROM Carts WHERE UserId=@UserId
        ", connection);

        getCartCmd.Parameters.AddWithValue("@UserId", userId);

        var result = await getCartCmd.ExecuteScalarAsync();

        // daca e null returnam lista goala
        if (result == null)
        {
            return new List<CartItemDto>();
        }

        int cartId = (int)result;

        // luam toate itemele din cart
        var cartItemsCmd = new SqlCommand(
            @"
            SELECT 
                ci.Id as CartItemId,
                ci.ProductId,
                p.ImageUrl,
                p.Description,
                p.Price,
                ci.Quantity,
                (p.Price * ci.Quantity) as Total
            FROM CartItems ci
            JOIN Products p
                ON ci.ProductId = p.Id
            WHERE ci.CartId = @CartId
            ", connection);
        cartItemsCmd.Parameters.AddWithValue("@CartId", cartId);

        using var reader = await cartItemsCmd.ExecuteReaderAsync();

        var items = new List<CartItemDto>();

        while (await reader.ReadAsync())
        {
            items.Add(new CartItemDto
            {
                CartItemId = (int)reader["CartItemId"],
                ProductId = (int)reader["ProductId"],
                ImageUrl = reader["ImageUrl"].ToString(),
                Description = reader["Description"].ToString(),
                Price = (decimal)reader["Price"],
                Quantity = (int)reader["Quantity"],
                Total = (decimal)reader["Total"]
            });
        }

        return items;
    }

    public async Task<CartItemDto> UpdateCartItem(int userId, int productId, int quantity)
    {
        using var connection = _databaseConnection.CreateConnection();
        await connection.OpenAsync();

        // luam cartId
        var getCartCmd = new SqlCommand(
            @"
            SELECT Id FROM Carts WHERE UserId=@UserId            
            ", connection);

        getCartCmd.Parameters.AddWithValue("@UserId", userId);

        var result = await getCartCmd.ExecuteScalarAsync();

        if (result == null)
        {
            return null;
        }

        int cartId = (int)result;

        // facem update la quantity
        var updateQuantity = new SqlCommand(
            @"
            UPDATE CartItems SET Quantity=@Quantity WHERE CartId=@CartId AND ProductId=@ProductId
            ", connection);

        updateQuantity.Parameters.AddWithValue("@CartId", cartId);
        updateQuantity.Parameters.AddWithValue("@ProductId", productId);
        updateQuantity.Parameters.AddWithValue("@Quantity", quantity);

        await updateQuantity.ExecuteNonQueryAsync();

        // returnam CartItem actualizat
        var selectCmd = new SqlCommand(
            @"
            SELECT 
                   ci.Id as CartItemId,
                   ci.ProductId,
                   p.ImageUrl,
                   p.Description,
                   p.Price,
                   ci.Quantity,
                   (p.Price * ci.Quantity) as Total
            FROM CartItems ci
            JOIN Products p
                ON ci.ProductId = p.Id
            WHERE ci.CartId=@CartId
            AND ci.ProductId=@ProductId
            ", connection);

        selectCmd.Parameters.AddWithValue("@CartId", cartId);
        selectCmd.Parameters.AddWithValue("@ProductId", productId);

        var readerCartItem = await selectCmd.ExecuteReaderAsync();

        CartItemDto item = null;

        if (await readerCartItem.ReadAsync())
        {
            item = new CartItemDto
            {
                CartItemId = (int)readerCartItem["CartItemId"],
                ProductId = (int)readerCartItem["ProductId"],
                ImageUrl = readerCartItem["ImageUrl"].ToString(),
                Description = readerCartItem["Description"].ToString(),
                Price = (decimal)readerCartItem["Price"],
                Quantity = (int)readerCartItem["Quantity"],
                Total = (decimal)readerCartItem["Total"]
            };
        }

        return item;
    }

    public async Task<bool> RemoveCartItem(int userId, int productId)
    {
        using var connection = _databaseConnection.CreateConnection();
        await connection.OpenAsync();

        // facem rost de Id-ul Cart-ului
        var getCartCmd = new SqlCommand(
            @"
            SELECT Id FROM Carts WHERE UserId=@UserId
            ", connection);

        getCartCmd.Parameters.AddWithValue("@UserId", userId);

        var result = await getCartCmd.ExecuteScalarAsync();

        if (result == null)
        {
            return false;
        }

        int cartId = (int)result;

        // acum stergem CartItem cu CartId si ProductId
        var deleteCmd = new SqlCommand(
            @"
            DELETE FROM CartItems WHERE CartId=@CartId AND ProductId=@ProductId
            ", connection);
        deleteCmd.Parameters.AddWithValue("@CartId", cartId);
        deleteCmd.Parameters.AddWithValue("@ProductId", productId);

        int affectedRows = await deleteCmd.ExecuteNonQueryAsync();

        return affectedRows > 0;
    }
}