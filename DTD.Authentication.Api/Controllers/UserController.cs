using DTD.Authentication.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DTD.Authentication.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> AllDtoById(int id)
        {
            var item = await _userService.AllDtoById(id);
            return Ok(new
            {
                User = item.user,
                Invoice = item.invoice,
                Product = item.product
            });
        }
    }
}
