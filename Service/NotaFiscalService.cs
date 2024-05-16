using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.NotaFiscal;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class NotaFiscalService : INotaFiscalInterface
    {
        private readonly BancoContext _bancoContext;

        public NotaFiscalService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<NotaFiscalModel>>> BuscarTodasNotasFiscais()
        {
            ServiceResponse<List<NotaFiscalModel>> serviceResponse = new ServiceResponse<List<NotaFiscalModel>>();

            try
            {
                var notasFiscais = await _bancoContext.NotaFiscal.Include(x => x.Fornecedor).ToListAsync();

                serviceResponse.dados = notasFiscais;
                serviceResponse.mensagem = "Todas as notas fiscais foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<NotaFiscalModel>> BuscarPorId(int id)
        {
            ServiceResponse<NotaFiscalModel> serviceResponse = new ServiceResponse<NotaFiscalModel>();

            try
            {
                var notasFiscais = await _bancoContext.NotaFiscal.Include(x => x.Fornecedor).FirstOrDefaultAsync(x => x.id == id);

                if (notasFiscais == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = notasFiscais;
                serviceResponse.mensagem = "Nota fiscal encontrada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<NotaFiscalModel>> CriarNotaFiscal(NotaFiscalCriacaoDto notaFiscalCriacaoDto)
        {
            ServiceResponse<NotaFiscalModel> serviceResponse = new ServiceResponse<NotaFiscalModel>();

            try
            {
                var notasFiscais = new NotaFiscalModel()
                {
                    dtaEntrada = notaFiscalCriacaoDto.dtaEntrada,
                    numero = notaFiscalCriacaoDto.numero,
                    idFornecedor = notaFiscalCriacaoDto.idFornecedor,
                    valor = notaFiscalCriacaoDto.valor,

                    dtaCadastroNota = DateTime.Now
                };

                _bancoContext.Add(notasFiscais);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = notasFiscais;
                serviceResponse.mensagem = "Nota fiscal cadastrada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<NotaFiscalModel>> AtualizarNotaFiscal(NotaFiscalEdicaoDto notaFiscalEdicaoDto)
        {
            ServiceResponse<NotaFiscalModel> serviceResponse = new ServiceResponse<NotaFiscalModel>();

            try
            {
                var notasFiscais = await _bancoContext.NotaFiscal.FirstOrDefaultAsync(x => x.id == notaFiscalEdicaoDto.id);

                if (notasFiscais == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                notasFiscais.dtaEntrada = notaFiscalEdicaoDto.dtaEntrada;
                notasFiscais.numero= notaFiscalEdicaoDto.numero;
                notasFiscais.idFornecedor = notaFiscalEdicaoDto.idFornecedor;
                notasFiscais.valor = notaFiscalEdicaoDto.valor;

                _bancoContext.Update(notasFiscais);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = notasFiscais;
                serviceResponse.mensagem = "Nota fiscal atualizada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<NotaFiscalModel>> DeletarNotaFiscal(int id)
        {
            ServiceResponse<NotaFiscalModel> serviceResponse = new ServiceResponse<NotaFiscalModel>();

            try
            {
                var notasFiscais = await _bancoContext.NotaFiscal.FirstOrDefaultAsync(x => x.id == id);

                if (notasFiscais == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                _bancoContext.NotaFiscal.Remove(notasFiscais);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = notasFiscais;
                serviceResponse.mensagem = "Nota fiscal excluído com sucesso!";

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
