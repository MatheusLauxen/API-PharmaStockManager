using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStock___API.Dto.Paciente;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteInterface _pacienteInterface;
        public PacienteController(IPacienteInterface pacienteInterface)
        {
            _pacienteInterface = pacienteInterface;
        }

        [HttpGet("BuscarTodosPacientes")]
        public async Task<ActionResult<ServiceResponse<List<PacienteModel>>>> BuscarTodosPacientes()
        {
            var pacientes = await _pacienteInterface.BuscarTodosPacientes();
            return Ok(pacientes);
        }

        [HttpGet("BuscarPacientePorId/{id}")]
        public async Task<ActionResult<ServiceResponse<PacienteModel>>> BuscarPacientePorId(int id)
        {
            var pacientes = await _pacienteInterface.BuscarPorId(id);
            return Ok(pacientes);
        }

        [HttpPost("AdicionarPaciente")]
        public async Task<ActionResult<ServiceResponse<PacienteModel>>> AdicionarPaciente(PacienteCriacaoDto pacienteCriacaoDto)
        {
            var pacientes = await _pacienteInterface.AdicionarPaciente(pacienteCriacaoDto);
            return Ok(pacientes);
        }

        [HttpPut("EditarPaciente")]
        public async Task<ActionResult<ServiceResponse<PacienteModel>>> EditarPaciente(PacienteModel pacienteModel)
        {
            var pacientes = await _pacienteInterface.AtualizarPaciente(pacienteModel);
            return Ok(pacientes);
        }

        [HttpDelete("DeletarPaciente")]
        public async Task<ActionResult<ServiceResponse<PacienteModel>>> DeletarPaciente(int id)
        {
            var pacientes = await _pacienteInterface.DeletarPaciente(id);
            return Ok(pacientes);
        }
    }
}
