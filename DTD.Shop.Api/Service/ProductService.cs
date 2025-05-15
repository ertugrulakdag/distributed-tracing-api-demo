using Dapper;
using DTD.Core.Context;
using DTD.Core.Helper;
using DTD.Model;
using System.Diagnostics;

namespace DTD.Shop.Api.Service
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _dapperContext;
        public ProductService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<ProductDto?> GetProductById(int productId)
        {
            using var activity = ShopTracing.ActivitySource.StartActivity("GetProductById Veri Hazırlama");
            activity?.AddEvent(new ActivityEvent("başladı"));
            using var connection = await _dapperContext.CreateConnectionAsync();
            var sql = "SELECT * FROM Products WHERE Id = @productId";
            var product = await connection.QueryFirstOrDefaultAsync<ProductDto>(sql, new { productId });
            activity?.AddEvent(new ActivityEvent("bitti"));
            return product;
        }
    }
}
