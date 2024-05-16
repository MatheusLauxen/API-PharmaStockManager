using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Medico;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoInterface _medicoInterface;
        public MedicoController(IMedicoInterface medicoInterface)
        {
            _medicoInterface = medicoInterface;
        }

        [HttpGet("BuscarTodosMedicos")]
        public async Task<ActionResult<ServiceResponse<List<MedicoModel>>>> BuscarTodosMedicos()
        {
            var medicos = await _medicoInterface.BuscarTodosMedicos();
            return Ok(medicos);
        }

        [HttpGet("BuscarMedicoPorId/{id}")]
        public async Task<ActionResult<ServiceResponse<MedicoModel>>> BuscarMedicoPorId(int id)
        {
            var medicos = await _medicoInterface.BuscarPorId(id);
            return Ok(medicos);
        }

        [HttpPost("AdicionarMedico")]
        public async Task<ActionResult<ServiceResponse<MedicoModel>>> AdicionarMedico(MedicoCriacaoDto medicoCriacaoDto)
        {
            var medicos = await _medicoInterface.AdicionarMedico(medicoCriacaoDto);
            return Ok(medicos);
        }

        [HttpPut("EditarMedico")]
        public async Task<ActionResult<ServiceResponse<MedicoModel>>> EditarMedico(MedicoModel medicoModel)
        {
            var medicos = await _medicoInterface.AtualizarMedico(medicoModel);
            return Ok(medicos);
        }

        [HttpDelete("DeletarMedico")]
        public async Task<ActionResult<ServiceResponse<MedicoModel>>> DeletarMedico(int id)
        {
            var medicos = await _medicoInterface.DeletarMedico(id);
            return Ok(medicos);
        }
    }
}
