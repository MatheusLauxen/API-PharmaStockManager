using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Produto;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoInterface _produtoInterface;
        public ProdutoController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

        [HttpGet("BuscarTodosProdutos")]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> BuscarTodosProdutos()
        {
            var produtos = await _produtoInterface.BuscarTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("BuscarProdutoPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> BuscarProdutoPorId(int id)
        {
            var produtos = await _produtoInterface.BuscarPorId(id);
            return Ok(produtos);
        }

        [HttpPost("CriarProduto")]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> CreateProduto(ProdutoCriacaoDto produtoCriacaoDto)
        {
            var produtos = await _produtoInterface.CriarProduto(produtoCriacaoDto);
            return Ok(produtos);
        }

        [HttpPut("EditarProduto")]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> UpdateProduto(ProdutoModel produtoModel)
        {
            var produtos = await _produtoInterface.AtualizarProduto(produtoModel);
            return Ok(produtos);
        }

        [HttpDelete("DeletarProduto")]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> DeleteProduto(int id)
        {
            var produtos = await _produtoInterface.DeletarProduto(id);
            return Ok(produtos);
        }
    }
}

