using BuildingBlocks.Exceptions;

namespace Catalog.API.Execption
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product not found.", Id)
        {
        }
    }
}
