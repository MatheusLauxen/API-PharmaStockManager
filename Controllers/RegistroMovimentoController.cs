using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Estoque;
using PharmaStock___API.Dto.RegistroMovimento;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroMovimentoController : ControllerBase
    {
        private readonly IRegistroMovimentoInterface _registroMovimentoInterface;
        public RegistroMovimentoController(IRegistroMovimentoInterface registroMovimentoInterface)
        {
            _registroMovimentoInterface = registroMovimentoInterface;
        }

        [HttpGet("BuscarTodasMovimentacoes")]
        [AuthorizePermission("Diretor")]
        public async Task<ActionResult<ServiceResponse<List<RegistroMovimentoModel>>>> BuscarTodasMovimentacoes()
        {
            var movimentacoes = await _registroMovimentoInterface.BuscarTodasMovimentacoes();
            return Ok(movimentacoes);
        }

        [HttpGet("BuscarMovimentacaoPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<RegistroMovimentoModel>>> BuscarMovimentacaoPorId(int id)
        {
            var movimentacoes = await _registroMovimentoInterface.BuscarPorId(id);
            return Ok(movimentacoes);
        }

        [HttpPost("AdicionarMovimentacao")]
        public async Task<ActionResult<ServiceResponse<RegistroMovimentoModel>>> AdicionarMovimentacao(RegistroMovimentoCriacaoDto registroMovimentoCriacaoDto)
        {
            var movimentacoes = await _registroMovimentoInterface.CriarMovimentacao(registroMovimentoCriacaoDto);
            return Ok(movimentacoes);
        }

        [HttpPut("EditarMovimentacao")]
        public async Task<ActionResult<ServiceResponse<RegistroMovimentoModel>>> EditarMovimentacao(RegistroMovimentoModel registroMovimentoModel)
        {
            var movimentacoes = await _registroMovimentoInterface.AtualizarMovimentacao(registroMovimentoModel);
            return Ok(movimentacoes);
        }

        [HttpDelete("DeletarMovimentacao")]
        public async Task<ActionResult<ServiceResponse<RegistroMovimentoModel>>> DeletarMovimentacao(int id)
        {
            var movimentacoes = await _registroMovimentoInterface.DeletarMovimentacao(id);
            return Ok(movimentacoes);
        }
    }
}
