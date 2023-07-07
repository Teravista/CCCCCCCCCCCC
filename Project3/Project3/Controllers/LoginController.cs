using Microsoft.AspNetCore.Mvc;
using Project3.Dtos;
using Project3.Models;
using Project3.Repositories;
using System.Net;

namespace Project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository loginRep;

        public LoginController(ILoginRepository loginRep)
        {
            this.loginRep = loginRep;
        }
        // PUT /category/login
        [HttpPut]
        public ActionResult Login(LoginDto loginDto)
        {
            var login = new LoginModel() { Login = loginDto.Login, Password = loginDto.Password };
            int results = loginRep.Login(login);
            if (results == 0)
            {
                var statusCode = HttpStatusCode.NotFound;
                var message = "logging in unsuccessful";
                return StatusCode((int)statusCode, message);
            }
            else
            {
                var statusCode = HttpStatusCode.OK;
                var message = "corectly logged in";
                return StatusCode((int)statusCode, message);
            }
        }
    }
}
