using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Produto;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly BancoContext _bancoContext;

        public ProdutoService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> BuscarTodosProdutos()
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();

            try
            {
                var produtos = await _bancoContext.Produto.ToListAsync();

                serviceResponse.dados = produtos;
                serviceResponse.mensagem = "Todos os produtos foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoModel>> BuscarPorId(int id)
        {
            ServiceResponse<ProdutoModel> serviceResponse = new ServiceResponse<ProdutoModel>();

            try
            {
                var produtos = await _bancoContext.Produto.FirstOrDefaultAsync(x => x.id == id);

                if (produtos == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = produtos;
                serviceResponse.mensagem = "Produto encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoModel>> CriarProduto(ProdutoCriacaoDto produtoCriacaoDto)
        {
            ServiceResponse<ProdutoModel> serviceResponse = new ServiceResponse<ProdutoModel>();

            try
            {
                var produtos = new ProdutoModel()
                {
                    nome = produtoCriacaoDto.nome,
                    dtaValidade = produtoCriacaoDto.dtaValidade,
                    nomeTipo = produtoCriacaoDto.nomeTipo,
                    descricao = produtoCriacaoDto.descricao,
                    fabricante = produtoCriacaoDto.fabricante
                };

                _bancoContext.Add(produtos);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtos;
                serviceResponse.mensagem = "Produto cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoModel>> AtualizarProduto(ProdutoModel produtoModel)
        {
            ServiceResponse<ProdutoModel> serviceResponse = new ServiceResponse<ProdutoModel>();

            try
            {
                var produtos = await _bancoContext.Produto.FirstOrDefaultAsync(x => x.id == produtoModel.id);

                if (produtos == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                produtos.nome = produtoModel.nome;
                produtos.dtaValidade = produtoModel.dtaValidade;
                produtos.nomeTipo = produtoModel.nomeTipo;
                produtos.descricao = produtoModel.descricao;
                produtos.fabricante = produtoModel.fabricante;

                _bancoContext.Update(produtos);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtos;
                serviceResponse.mensagem = "Produto atualizado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoModel>> DeletarProduto(int id)
        {
            ServiceResponse<ProdutoModel> serviceResponse = new ServiceResponse<ProdutoModel>();

            try
            {
                var produtos = await _bancoContext.Produto.FirstOrDefaultAsync(x => x.id == id);

                if (produtos == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                var produtoNaTabelaEstoque = _bancoContext.Estoque.FirstOrDefault(me => me.idProduto == id);
                var produtoNaTabelaRegistroMovimento = _bancoContext.RegistroMovimento.FirstOrDefault(me => me.idProduto == id);

                if (produtoNaTabelaEstoque != null || produtoNaTabelaRegistroMovimento != null)
                {
                    serviceResponse.mensagem = "O produto selecionado não pode ser excluído porque está em uso em outra tabela.";
                    return serviceResponse;
                }

                _bancoContext.Produto.Remove(produtos);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtos;
                serviceResponse.mensagem = "Produto excluído com sucesso!";

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
