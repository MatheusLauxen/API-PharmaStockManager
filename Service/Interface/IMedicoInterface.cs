using PharmaStock___API.Dto.Medico;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IMedicoInterface
    {
        Task<ServiceResponse<List<MedicoModel>>> BuscarTodosMedicos();
        Task<ServiceResponse<MedicoModel>> BuscarPorId(int id);
        Task<ServiceResponse<MedicoModel>> AdicionarMedico(MedicoCriacaoDto medicoCriacaoDto);
        Task<ServiceResponse<MedicoModel>> AtualizarMedico(MedicoModel medicoModel);
        Task<ServiceResponse<MedicoModel>> DeletarMedico(int id);
    }
}
