using PharmaStock___API.Dto.Produto;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IProdutoInterface
    {
        Task<ServiceResponse<List<ProdutoModel>>> BuscarTodosProdutos();
        Task<ServiceResponse<ProdutoModel>> BuscarPorId(int id);
        Task<ServiceResponse<ProdutoModel>> CriarProduto(ProdutoCriacaoDto produtoCriacaoDto);
        Task<ServiceResponse<ProdutoModel>> AtualizarProduto(ProdutoModel produtoModel);
        Task<ServiceResponse<ProdutoModel>> DeletarProduto(int id);
    }
}
