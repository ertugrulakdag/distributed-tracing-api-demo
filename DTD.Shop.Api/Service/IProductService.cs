using DTD.Model;

namespace DTD.Shop.Api.Service
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductById(int productId);
    }
}
