using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Fornecedor;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorInterface _fornecedorInterface;
        public FornecedorController(IFornecedorInterface fornecedorInterface)
        {
            _fornecedorInterface = fornecedorInterface;
        }

        [HttpGet("BuscarTodosFornecedores")]
        public async Task<ActionResult<ServiceResponse<List<FornecedorModel>>>> BuscarTodosFornecedores()
        {
            var fornecedores = await _fornecedorInterface.BuscarTodosFornecedores();
            return Ok(fornecedores);
        }

        [HttpGet("BuscarFornecedorPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<FornecedorModel>>> BuscarFornecedorPorId(int id)
        {
            var fornecedores = await _fornecedorInterface.BuscarPorId(id);
            return Ok(fornecedores);
        }

        [HttpPost("CriarFornecedor")]
        public async Task<ActionResult<ServiceResponse<FornecedorModel>>> CreateFornecedor(FornecedorCriacaoDto fornecedorCriacaoDto)
        {
            var fornecedores = await _fornecedorInterface.CriarFornecedor(fornecedorCriacaoDto);
            return Ok(fornecedores);
        }

        [HttpPut("EditarFornecedor")]
        public async Task<ActionResult<ServiceResponse<FornecedorModel>>> UpdateFornecedor(FornecedorModel fornecedorModel)
        {
            var fornecedores = await _fornecedorInterface.AtualizarFornecedor(fornecedorModel);
            return Ok(fornecedores);
        }

        [HttpDelete("DeletarFornecedor")]
        public async Task<ActionResult<ServiceResponse<FornecedorModel>>> DeleteFornecedor(int id)
        {
            var fornecedores = await _fornecedorInterface.DeletarFornecedor(id);
            return Ok(fornecedores);
        }
    }
}
