namespace Ecommerce.Api.Infrastructure.Repositories.CheckoutRepository;

using DefaultNamespace;
using Domain.Checkout;
using Microsoft.Data.SqlClient;

public class CheckoutRepository: ICheckoutRepository
{
    private readonly Db _databaseConnection;

    public CheckoutRepository(Db databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task<OrderDto> ProcessCheckout(CheckoutRequest request)
    {
        using var connection = _databaseConnection.CreateConnection();
        await connection.OpenAsync();
        
        //1. luam cartId
        var getCartCmd = new SqlCommand(
            @"SELECT Id FROM Carts WHERE UserId = @UserId",connection);
        
        getCartCmd.Parameters.AddWithValue("@UserId", request.UserId);

        var cartIdObj = await getCartCmd.ExecuteScalarAsync();

        if (cartIdObj == null)
        {
            throw new Exception("Cart not found");
        }
        
        ////////////////////////// CARTD ID //////////////////////////
        int cartId = (int)cartIdObj;
        ////////////////////////// CARTD ID ////////////////////////// 
        
        //2. luam produsele din cart
        var getItemsFromCartCmd = new SqlCommand(
            @"
            select ci.ProductId, ci.Quantity, p.Price, p.Name
            from CartItems ci
            join Products p on ci.ProductId = p.Id
            where ci.CartId = @CartId
            ", connection);

        getItemsFromCartCmd.Parameters.AddWithValue("@CartId", cartId);

        var items = new List<(int ProductId, int Quantity, decimal Price, string Name)>();

        using (var reader = await getItemsFromCartCmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                items.Add((
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetDecimal(2),
                    reader.GetString(3)
                    ));
            }
        }

        if (!items.Any())
        {
            throw new Exception("Cart is empty");
        }

        //3. calculam total
        decimal total = items.Sum(x => x.Price * x.Quantity);

        //4. cream order
        var insertOrderCmd = new SqlCommand(
            @"
            insert into Orders
            (UserId, TotalAmount, ShippingName, ShippingAddress, ShippingCity, ShippingPostalCode)
            OUTPUT INSERTED.Id
            value
            (@UserId, @Total, @Name, @Address, @City, @Postal)
            ", connection);

        insertOrderCmd.Parameters.AddWithValue("@UserId", request.UserId);
        insertOrderCmd.Parameters.AddWithValue("@Total", total);
        insertOrderCmd.Parameters.AddWithValue("@Name", request.ShippingName);
        insertOrderCmd.Parameters.AddWithValue("@Address", request.ShippingAddress);
        insertOrderCmd.Parameters.AddWithValue("@City", request.ShippingCity);
        insertOrderCmd.Parameters.AddWithValue("@Postal", request.ShippingPostalCode);

        int orderId = (int)await insertOrderCmd.ExecuteScalarAsync();

        //5. cream orderItems
        var orderItems = new List<OrderItemDto>();

        foreach (var item in items)
        {
            var insertedItemCmd = new SqlCommand(
                @"
                    INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
                VALUES (@OrderId, @ProductId, @Qty, @Price)
                ", connection);

            insertedItemCmd.Parameters.AddWithValue("@OrderId", orderId);
            insertedItemCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
            insertedItemCmd.Parameters.AddWithValue("@Qty", item.Quantity);
            insertedItemCmd.Parameters.AddWithValue("@Price", item.Price);

            await insertedItemCmd.ExecuteNonQueryAsync();
            
            orderItems.Add(new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price
            });
        }

        //6. golim cart-ul
        var clearCartCmd = new SqlCommand(
            @"
            DELETE FROM Carts WHERE CartId = @CartId
            ",connection);

        clearCartCmd.Parameters.AddWithValue("@CartId", cartId);

        await clearCartCmd.ExecuteNonQueryAsync();

        //7. return snapshot orderDto
        return new OrderDto
        {
            OrderId = orderId,
            TotalAmount = total,
            ShippingName = request.ShippingName,
            ShippingAddress = request.ShippingAddress,
            ShippingCity = request.ShippingCity,
            ShippingPostalCode = request.ShippingPostalCode,
            Items = orderItems
        };

    }
}