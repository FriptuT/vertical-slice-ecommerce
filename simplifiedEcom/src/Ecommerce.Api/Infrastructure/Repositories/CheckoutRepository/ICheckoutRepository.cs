namespace Ecommerce.Api.Infrastructure.Repositories.CheckoutRepository;

using Domain.Checkout;

public interface ICheckoutRepository
{
    Task<OrderDto> ProcessCheckout(CheckoutRequest request);
}