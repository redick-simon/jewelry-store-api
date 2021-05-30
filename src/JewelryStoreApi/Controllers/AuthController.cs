using JewelryStoreApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace JewelryStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            StringValues value;
            HttpContext.Request.Headers.TryGetValue("Authorization", out value);

            var validationresult = _userService.Validate(value.ToString());

            if(validationresult.Valid)
            {
                return Ok(validationresult);
            }

            return Unauthorized(validationresult);

        }
    }
}
