using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenJwtBearer.Models;
using TokenJwtBearer.Services;

namespace TokenJwtBearer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }


        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetUsers()
            => Ok(_service.ListUsers());


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody]Usuario model)
        {
            var user = _service.GetUser(model);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            user.Password = "";
            user.Token = _service.GetTokenUserLogin(user);

            return Ok(user);
        }


        /*
            Para logar basta colocar no header Valor = Autorization | Chave = Bearer kbdcug cgsidygd agyvbcysdufyagv 
        */


        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet("employee")]
        [Authorize(Roles = "admin,user")]
        public string Employee() => "Funcionário";

        [HttpGet("manager")]
        [Authorize(Roles = "admin")]
        public string Manager() => "Gerente";
    }
}