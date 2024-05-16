using PharmaStock___API.Dto.ProdutoNota;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IProdutoNotaInterface
    {
        Task<ServiceResponse<List<ProdutoNotaModel>>> BuscarTodosProdutoNota();
        Task<ServiceResponse<ProdutoNotaModel>> BuscarPorId(int id);
        Task<ServiceResponse<ProdutoNotaModel>> CriarProdutoNota(ProdutoNotaCriacaoDto produtoNotaCriacaoDto);
        Task<ServiceResponse<ProdutoNotaModel>> DeletarProdutoNota(int id);
    }
}
