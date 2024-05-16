using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.ProdutoNota;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class ProdutoNotaService : IProdutoNotaInterface
    {
        private readonly BancoContext _bancoContext;

        public ProdutoNotaService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<ProdutoNotaModel>>> BuscarTodosProdutoNota()
        {
            ServiceResponse<List<ProdutoNotaModel>> serviceResponse = new ServiceResponse<List<ProdutoNotaModel>>();

            try
            {
                var produtosNota = await _bancoContext.ProdutoNota.Include(x => x.NotaFiscal).Include(x => x.Produto).ToListAsync();

                serviceResponse.dados = produtosNota;
                serviceResponse.mensagem = "Todos os produtosNotas foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoNotaModel>> BuscarPorId(int id)
        {
            ServiceResponse<ProdutoNotaModel> serviceResponse = new ServiceResponse<ProdutoNotaModel>();

            try
            {
                var produtosNota = await _bancoContext.ProdutoNota.Include(x => x.NotaFiscal).Include(x => x.Produto).FirstOrDefaultAsync(x => x.id == id);

                if (produtosNota == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = produtosNota;
                serviceResponse.mensagem = "ProdutoNota encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoNotaModel>> CriarProdutoNota(ProdutoNotaCriacaoDto produtoNotaCriacaoDto)
        {
            ServiceResponse<ProdutoNotaModel> serviceResponse = new ServiceResponse<ProdutoNotaModel>();

            try
            {
                var produtosNota = new ProdutoNotaModel()
                {
                    idNota = produtoNotaCriacaoDto.idNota,
                    idProduto = produtoNotaCriacaoDto.idProduto,
                    quantidade = produtoNotaCriacaoDto.quantidade,
                    lote = produtoNotaCriacaoDto.lote
                };

                _bancoContext.Add(produtosNota);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtosNota;
                serviceResponse.mensagem = "ProdutoNota cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ProdutoNotaModel>> DeletarProdutoNota(int id)
        {
            ServiceResponse<ProdutoNotaModel> serviceResponse = new ServiceResponse<ProdutoNotaModel>();

            try
            {
                var produtosNota = await _bancoContext.ProdutoNota.FirstOrDefaultAsync(x => x.id == id);

                if (produtosNota == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                _bancoContext.ProdutoNota.Remove(produtosNota);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = produtosNota;
                serviceResponse.mensagem = "ProdutoNota excluído com sucesso!";

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
