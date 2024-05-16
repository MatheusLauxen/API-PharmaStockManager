using PharmaStock___API.Dto.Estoque;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IEstoqueInterface
    {
        Task<ServiceResponse<List<EstoqueModel>>> BuscarTodoEstoque();
        Task<ServiceResponse<EstoqueModel>> BuscarPorId(int id);
        Task<ServiceResponse<EstoqueModel>> AdicionarNoEstoque(EstoqueCriacaoDto estoqueCriacaoDto);
        Task<ServiceResponse<EstoqueModel>> AtualizarEstoque(EstoqueModel estoqueModel);
        Task<ServiceResponse<EstoqueModel>> DeletarNoEstoque(int id);
    }
}
