using GroupChat.API.Interfaces;
using GroupChat.ViewModels.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Controllers
{
    [EnableCors("AllowOrigin")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("api/ValidateLogin")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginModel LoginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _loginService.ValidateLogin(LoginRequest);
            return response;
        }
    }
}
