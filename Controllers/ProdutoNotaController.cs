using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.ProdutoNota;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoNotaController : ControllerBase
    {
        private readonly IProdutoNotaInterface _produtoNotaInterface;
        public ProdutoNotaController(IProdutoNotaInterface produtoNotaInterface)
        {
            _produtoNotaInterface = produtoNotaInterface;
        }

        [HttpGet("BuscarTodosProdutoNota")]
        [AuthorizePermission("Diretor")]
        public async Task<ActionResult<ServiceResponse<List<ProdutoNotaModel>>>> BuscarTodosProdutoNota()
        {
            var produtosNota = await _produtoNotaInterface.BuscarTodosProdutoNota();
            return Ok(produtosNota);
        }

        [HttpGet("BuscarProdutoNotaPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<ProdutoNotaModel>>> BuscarProdutoNotaPorId(int id)
        {
            var produtoNota = await _produtoNotaInterface.BuscarPorId(id);
            return Ok(produtoNota);
        }

        [HttpPost("AdicionarProdutoNota")]
        public async Task<ActionResult<ServiceResponse<ProdutoNotaModel>>> AdicionarProdutoNota(ProdutoNotaCriacaoDto produtoNotaCriacaoDto)
        {
            var produtoNota = await _produtoNotaInterface.CriarProdutoNota(produtoNotaCriacaoDto);
            return Ok(produtoNota);
        }

        [HttpDelete("DeletarProdutoNota")]
        public async Task<ActionResult<ServiceResponse<ProdutoNotaModel>>> DeletarProdutoNota(int id)
        {
            var produtoNota = await _produtoNotaInterface.DeletarProdutoNota(id);
            return Ok(produtoNota);
        }
    }
}
