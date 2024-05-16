using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.NotaFiscal;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalInterface _notaFiscalInterface;
        public NotaFiscalController(INotaFiscalInterface notaFiscalInterface)
        {
            _notaFiscalInterface = notaFiscalInterface;
        }

        [HttpGet("BuscarTodasNotasFiscais")]
        public async Task<ActionResult<ServiceResponse<List<NotaFiscalModel>>>> BuscarTodasNotasFiscais()
        {
            var notasFiscais = await _notaFiscalInterface.BuscarTodasNotasFiscais();
            return Ok(notasFiscais);
        }

        [HttpGet("BuscarNotaFiscalPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<NotaFiscalModel>>> BuscarNotaFiscalPorId(int id)
        {
            var notasFiscais = await _notaFiscalInterface.BuscarPorId(id);
            return Ok(notasFiscais);
        }

        [HttpPost("CriarNotaFiscal")]
        public async Task<ActionResult<ServiceResponse<NotaFiscalModel>>> CreateNotaFiscal(NotaFiscalCriacaoDto notaFiscalCriacaoDto)
        {
            var notasFiscais = await _notaFiscalInterface.CriarNotaFiscal(notaFiscalCriacaoDto);
            return Ok(notasFiscais);
        }

        [HttpPut("EditarNotaFiscal")]
        public async Task<ActionResult<ServiceResponse<NotaFiscalModel>>> UpdateNotaFiscal(NotaFiscalEdicaoDto notaFiscalEdicaoDto)
        {
            var notasFiscais = await _notaFiscalInterface.AtualizarNotaFiscal(notaFiscalEdicaoDto);
            return Ok(notasFiscais);
        }

        [HttpDelete("DeletarNotaFiscal")]
        public async Task<ActionResult<ServiceResponse<NotaFiscalModel>>> DeleteNotaFiscal(int id)
        {
            var notasFiscais = await _notaFiscalInterface.DeletarNotaFiscal(id);
            return Ok(notasFiscais);
        }
    }
}

