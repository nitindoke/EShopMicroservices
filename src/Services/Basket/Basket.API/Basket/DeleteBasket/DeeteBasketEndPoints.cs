
namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeeteBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));
                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);

            }).WithName("DeleteBasket").WithTags("Basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a user's basket")
            .WithDescription("Deletes the basket for the specified user. Returns true if the deletion was successful, otherwise false.");


        }
    }
}
