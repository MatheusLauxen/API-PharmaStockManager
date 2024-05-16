using PharmaStock___API.Dto.RegistroMovimento;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IRegistroMovimentoInterface
    {
        Task<ServiceResponse<List<RegistroMovimentoModel>>> BuscarTodasMovimentacoes();
        Task<ServiceResponse<RegistroMovimentoModel>> BuscarPorId(int id);
        Task<ServiceResponse<RegistroMovimentoModel>> CriarMovimentacao(RegistroMovimentoCriacaoDto registroMovimentoCriacaoDto);
        Task<ServiceResponse<RegistroMovimentoModel>> AtualizarMovimentacao(RegistroMovimentoModel registroMovimentoModel);
        Task<ServiceResponse<RegistroMovimentoModel>> DeletarMovimentacao(int id);
    }
}
