
namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string UserName); 

public record GetBasketResponse(ShoppingCart Cart);
//public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule //Carter
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
                                                               //ISender --> Mediator        
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));

            //Adapt method comes from Mappster
            var respose = result.Adapt<GetBasketResponse>();

            return Results.Ok(respose);
        })
        .WithName("GetProductById")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
