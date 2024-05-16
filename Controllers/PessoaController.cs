using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Estoque;
using PharmaStock___API.Dto.Pessoa;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaInterface _pessoaInterface;
        public PessoaController(IPessoaInterface pessoaInterface)
        {
            _pessoaInterface = pessoaInterface;
        }

        [HttpGet("BuscarTodasPessoas")]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> BuscarTodasPessoas()
        {
            var pessoas = await _pessoaInterface.BuscarTodasPessoas();
            return Ok(pessoas);
        }

        [HttpGet("BuscarPessoaPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<PessoaModel>>> BuscarPessoaPorId(int id)
        {
            var pessoas = await _pessoaInterface.BuscarPorId(id);
            return Ok(pessoas);
        }

        [HttpPost("AdicionarPessoa")]
        public async Task<ActionResult<ServiceResponse<PessoaModel>>> AdicionarPessoa(PessoaCriacaoDto pessoaCriacaoDto)
        {
            var pessoas = await _pessoaInterface.CriarPessoa(pessoaCriacaoDto);
            return Ok(pessoas);
        }

        [HttpPut("EditarPessoa")]
        public async Task<ActionResult<ServiceResponse<PessoaModel>>> EditarPessoa(PessoaEdicaoDto pessoaEdicaoDto)
        {
            var pessoas = await _pessoaInterface.AtualizarPessoa(pessoaEdicaoDto);
            return Ok(pessoas);
        }

        [HttpDelete("DeletarPessoa")]
        public async Task<ActionResult<ServiceResponse<PessoaModel>>> DeletarPessoa(int id)
        {
            var pessoas = await _pessoaInterface.DeletarPessoa(id);
            return Ok(pessoas);
        }
    }
}
