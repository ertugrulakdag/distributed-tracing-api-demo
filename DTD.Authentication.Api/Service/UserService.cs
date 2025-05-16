using Dapper;
using DTD.Authentication.Api.Service;
using DTD.Core.Clients;
using DTD.Core.Context;
using DTD.Core.Helper;
using DTD.Model;
using System.Diagnostics;

namespace DTD.Service
{
    public class UserService : IUserService
    {
        private readonly DapperContext _dapperContext;
        private readonly ShopClient _productClient;
        private readonly InvoiceClient _invoiceClient;

        public UserService(DapperContext dapperContext, ShopClient productClient, InvoiceClient invoiceClient)
        {
            _dapperContext = dapperContext;
            _productClient = productClient;
            _invoiceClient = invoiceClient;
        }

        public async Task<UserDto?> GetUserById(int userId)
        {
            using var activity = AuthenticationTracing.ActivitySource.StartActivity("GetUserById Veri Hazırlama");
            activity?.SetTag("user.id", userId);
            activity?.AddEvent(new ActivityEvent($"{userId}:başladı"));
            await Task.Delay(300);
            var user= new UserDto()
            {
                CreatedAt = DateTime.Now,
                Id = userId
            };
            activity?.AddEvent(new ActivityEvent($"{userId}:bitti"));
            return user;
        }
        public async Task<(UserDto? user, InvoiceDto? invoice, ProductDto? product)> AllDtoById(int id)
        {
            using var connection = await _dapperContext.CreateConnectionAsync();
            var sql = "SELECT * FROM Users WHERE Id = @id";
            var user = await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { id });
            var product = await _productClient.GetProductAsync(id);
            var invoice = await _invoiceClient.GetInvoiceAsync(id);
            return (user, invoice, product);
        }

    }
}
