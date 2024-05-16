using PharmaStock___API.Dto.Fornecedor;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IFornecedorInterface
    {
        Task<ServiceResponse<List<FornecedorModel>>> BuscarTodosFornecedores();
        Task<ServiceResponse<FornecedorModel>> BuscarPorId(int id);
        Task<ServiceResponse<FornecedorModel>> CriarFornecedor(FornecedorCriacaoDto fornecedorCriacaoDto);
        Task<ServiceResponse<FornecedorModel>> AtualizarFornecedor(FornecedorModel fornecedorModel);
        Task<ServiceResponse<FornecedorModel>> DeletarFornecedor(int id);
    }
}
