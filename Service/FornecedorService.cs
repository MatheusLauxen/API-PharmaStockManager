using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Fornecedor;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class FornecedorService : IFornecedorInterface
    {
        private readonly BancoContext _bancoContext;

        public FornecedorService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<FornecedorModel>>> BuscarTodosFornecedores()
        {
            ServiceResponse<List<FornecedorModel>> serviceResponse = new ServiceResponse<List<FornecedorModel>>();

            try
            {
                var forncedores = await _bancoContext.Fornecedor.ToListAsync();

                serviceResponse.dados = forncedores;
                serviceResponse.mensagem = "Todos os fornecedores foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<FornecedorModel>> BuscarPorId(int id)
        {
            ServiceResponse<FornecedorModel> serviceResponse = new ServiceResponse<FornecedorModel>();

            try
            {
                var fornecedores = await _bancoContext.Fornecedor.FirstOrDefaultAsync(x => x.id == id);

                if (fornecedores == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = fornecedores;
                serviceResponse.mensagem = "Fornecedor encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<FornecedorModel>> CriarFornecedor(FornecedorCriacaoDto fornecedorCriacaoDto)
        {
            ServiceResponse<FornecedorModel> serviceResponse = new ServiceResponse<FornecedorModel>();

            try
            {
                var fornecedores = new FornecedorModel()
                {
                    nome = fornecedorCriacaoDto.nome,
                    cnpj = fornecedorCriacaoDto.cnpj,
                    telefone = fornecedorCriacaoDto.telefone,
                    endereco = fornecedorCriacaoDto.endereco
                };

                _bancoContext.Add(fornecedores);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = fornecedores;
                serviceResponse.mensagem = "Fornecedor cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<FornecedorModel>> AtualizarFornecedor(FornecedorModel fornecedorModel)
        {
            ServiceResponse<FornecedorModel> serviceResponse = new ServiceResponse<FornecedorModel>();

            try
            {
                var fornecedores = await _bancoContext.Fornecedor.FirstOrDefaultAsync(x => x.id == fornecedorModel.id);

                if (fornecedores == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                fornecedores.nome= fornecedorModel.nome;
                fornecedores.cnpj = fornecedorModel.cnpj;
                fornecedores.telefone = fornecedorModel.telefone;
                fornecedores.endereco = fornecedorModel.endereco;

                _bancoContext.Update(fornecedores);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = fornecedores;
                serviceResponse.mensagem = "Fornecedor atualizado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<FornecedorModel>> DeletarFornecedor(int id)
        {
            ServiceResponse<FornecedorModel> serviceResponse = new ServiceResponse<FornecedorModel>();

            try
            {
                var fornecedores = await _bancoContext.Fornecedor.FirstOrDefaultAsync(x => x.id == id);

                if (fornecedores == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                var fornecedorNaTabelaNotaFiscal = _bancoContext.NotaFiscal.FirstOrDefault(me => me.idFornecedor == id);

                if (fornecedorNaTabelaNotaFiscal != null)
                {
                    serviceResponse.mensagem = "O fornecedor selecionado não pode ser excluído porque está em uso em outra tabela.";
                    return serviceResponse;
                }

                _bancoContext.Fornecedor.Remove(fornecedores);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = fornecedores;
                serviceResponse.mensagem = "Fornecedor excluído com sucesso!";

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
