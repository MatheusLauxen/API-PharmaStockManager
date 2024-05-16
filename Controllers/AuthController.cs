using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Auth;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<ServiceResponse<UsuarioModel>>> CreateUsuario(AuthCriacaoDto authCriacaoDto)
        {
            var usuario = await _authInterface.CreateAccount(authCriacaoDto);
            return Ok(usuario);
        }

        [HttpPost("AutenticarUsuario")]
        public async Task<ActionResult> AuthenticateUser(AuthEntrar authEntrar)
        {
            var resposta = await _authInterface.authenticateUser(authEntrar);
            return Ok(resposta);
        }
    }
}
