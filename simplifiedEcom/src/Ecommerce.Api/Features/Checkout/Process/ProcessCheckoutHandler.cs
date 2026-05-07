namespace Ecommerce.Api.Features.Checkout.Process;

using Domain.Checkout;
using Infrastructure.Repositories.CheckoutRepository;
using Microsoft.AspNetCore.Http.HttpResults;

public class ProcessCheckoutHandler
{
    private readonly ICheckoutRepository _repository;

    public ProcessCheckoutHandler(ICheckoutRepository repository)
    {
        _repository = repository;
    }
    public async Task<IResult> Handle(CheckoutRequest request)
    {
        var order = await _repository.ProcessCheckout(request);

        return Results.Ok(order);
    }
}