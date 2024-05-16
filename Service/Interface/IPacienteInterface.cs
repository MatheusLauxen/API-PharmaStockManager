using PharmaStock___API.Dto.Paciente;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IPacienteInterface
    {
        Task<ServiceResponse<List<PacienteModel>>> BuscarTodosPacientes();
        Task<ServiceResponse<PacienteModel>> BuscarPorId(int id);
        Task<ServiceResponse<PacienteModel>> AdicionarPaciente(PacienteCriacaoDto pacienteCriacaoDto);
        Task<ServiceResponse<PacienteModel>> AtualizarPaciente(PacienteModel pacienteModel);
        Task<ServiceResponse<PacienteModel>> DeletarPaciente(int id);
    }
}
