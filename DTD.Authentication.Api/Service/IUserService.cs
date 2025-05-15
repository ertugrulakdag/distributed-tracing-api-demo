using DTD.Model;

namespace DTD.Authentication.Api.Service
{
    public interface IUserService
    {
        Task<UserDto?> GetUserById(int userId);
        Task<(UserDto? user, InvoiceDto? invoice, ProductDto? product)> AllDtoById(int id);
    }
}
