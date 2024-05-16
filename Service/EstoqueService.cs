using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Estoque;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class EstoqueService : IEstoqueInterface
    {
        private readonly BancoContext _bancoContext;

        public EstoqueService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<EstoqueModel>>> BuscarTodoEstoque()
        {
            ServiceResponse<List<EstoqueModel>> serviceResponse = new ServiceResponse<List<EstoqueModel>>();

            try
            {
                var produtosEstoque = await _bancoContext.Estoque.Include(x => x.Produto).ToListAsync();

                serviceResponse.dados = produtosEstoque;
                serviceResponse.mensagem = "Todos os produtos do estoque foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<EstoqueModel>> BuscarPorId(int id)
        {
            ServiceResponse<EstoqueModel> serviceResponse = new ServiceResponse<EstoqueModel>();

            try
            {
                var produtosEstoque = await _bancoContext.Estoque.Include(x => x.Produto).FirstOrDefaultAsync(x => x.id == id);

                if (produtosEstoque == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = produtosEstoque;
                serviceResponse.mensagem = "Produto do estoque encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<EstoqueModel>> AdicionarNoEstoque(EstoqueCriacaoDto estoqueCriacaoDto)
        {
            ServiceResponse<EstoqueModel> serviceResponse = new ServiceResponse<EstoqueModel>();

            try
            {
                var produtosEstoque = new EstoqueModel()
                {
                    idProduto = estoqueCriacaoDto.idProduto,
                    quantidade = estoqueCriacaoDto.quantidade
                };

                _bancoContext.Add(produtosEstoque);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtosEstoque;
                serviceResponse.mensagem = "Produto cadastrado no estoque com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<EstoqueModel>> AtualizarEstoque(EstoqueModel estoqueModel)
        {
            ServiceResponse<EstoqueModel> serviceResponse = new ServiceResponse<EstoqueModel>();

            try
            {
                var produtosEstoque = await _bancoContext.Estoque.FirstOrDefaultAsync(x => x.id == estoqueModel.id);

                if (produtosEstoque == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                produtosEstoque.idProduto = estoqueModel.idProduto;
                produtosEstoque.quantidade = estoqueModel.quantidade;

                _bancoContext.Update(produtosEstoque);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtosEstoque;
                serviceResponse.mensagem = "Produto do estoque atualizado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<EstoqueModel>> DeletarNoEstoque(int id)
        {
            ServiceResponse<EstoqueModel> serviceResponse = new ServiceResponse<EstoqueModel>();

            try
            {
                var produtosEstoque = await _bancoContext.Estoque.FirstOrDefaultAsync(x => x.id == id);

                if (produtosEstoque == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                _bancoContext.Estoque.Remove(produtosEstoque);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtosEstoque;
                serviceResponse.mensagem = "Produto excluído do estoque com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }
    }
}
