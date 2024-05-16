using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.PermissaoUsuario;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissaoUsuarioController : ControllerBase
    {
        private readonly IPermissaoUsuarioInterface _permissaoUsuarioInterface;
        public PermissaoUsuarioController(IPermissaoUsuarioInterface permissaoUsuarioInterface)
        {
            _permissaoUsuarioInterface = permissaoUsuarioInterface;
        }

        [HttpGet("BuscarTodasPermissoes")]
        public async Task<ActionResult<ServiceResponse<List<PermissaoUsuarioModel>>>> BuscarTodasPermissoes()
        {
            var permissoes = await _permissaoUsuarioInterface.BuscarTodasPermissoes();
            return Ok(permissoes);
        }

        [HttpGet("BuscarPermissaoPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<PermissaoUsuarioModel>>> BuscarPermissaoPorId(int id)
        {
            var permissoes = await _permissaoUsuarioInterface.BuscarPorId(id);
            return Ok(permissoes);
        }

        [HttpPost("CriarPermissao")]
        public async Task<ActionResult<ServiceResponse<PacienteModel>>> CriarPermissao(PermissaoUsuarioCriacaoDto permissaoUsuarioCriacaoDto)
        {
            var permissoes = await _permissaoUsuarioInterface.CriarPermissao(permissaoUsuarioCriacaoDto);
            return Ok(permissoes);
        }

        [HttpPut("EditarPermissao")]
        public async Task<ActionResult<ServiceResponse<PermissaoUsuarioModel>>> EditarPermissao(PermissaoUsuarioModel permissaoUsuarioModel)
        {
            var permissoes = await _permissaoUsuarioInterface.AtualizarPermissao(permissaoUsuarioModel);
            return Ok(permissoes);
        }

        [HttpDelete("DeletarPermissao")]
        public async Task<ActionResult<ServiceResponse<PermissaoUsuarioModel>>> DeletarPermissao(int id)
        {
            var permissoes = await _permissaoUsuarioInterface.DeletarPermissao(id);
            return Ok(permissoes);
        }
    }
}
