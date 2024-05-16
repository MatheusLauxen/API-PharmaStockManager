using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Estoque;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueInterface _estoqueInterface;
        public EstoqueController(IEstoqueInterface estoqueInterface)
        {
            _estoqueInterface = estoqueInterface;
        }

        [HttpGet("BuscarTodosProdutosNoEstoque")]
        [AuthorizePermission("Diretor")]
        public async Task<ActionResult<ServiceResponse<List<EstoqueModel>>>> BuscarTodosProdutosNoEstoque()
        {
            var produtosEstoque = await _estoqueInterface.BuscarTodoEstoque();
            return Ok(produtosEstoque);
        }

        [HttpGet("BuscarProdutoNoEstoquePorId/{id}")]
        public async Task<ActionResult<ServiceResponse<EstoqueModel>>> BuscarProdutoNoEstoquePorId(int id)
        {
            var produtosEstoque = await _estoqueInterface.BuscarPorId(id);
            return Ok(produtosEstoque);
        }

        [HttpPost("AdicionarProdutoNoEstoque")]
        public async Task<ActionResult<ServiceResponse<EstoqueModel>>> AdicionarProdutoNoEstoque(EstoqueCriacaoDto estoqueCriacaoDto)
        {
            var produtosEstoque = await _estoqueInterface.AdicionarNoEstoque(estoqueCriacaoDto);
            return Ok(produtosEstoque);
        }

        [HttpPut("EditarProdutoNoEstoque")]
        public async Task<ActionResult<ServiceResponse<EstoqueModel>>> EditarProdutoNoEstoque(EstoqueModel estoqueModel)
        {
            var produtosEstoque = await _estoqueInterface.AtualizarEstoque(estoqueModel);
            return Ok(produtosEstoque);
        }

        [HttpDelete("DeletarProdutoDoEstoque")]
        public async Task<ActionResult<ServiceResponse<EstoqueModel>>> DeletarProdutoDoEstoque(int id)
        {
            var produtosEstoque = await _estoqueInterface.DeletarNoEstoque(id);
            return Ok(produtosEstoque);
        }
    }
}

