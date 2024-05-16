using PharmaStock___API.Dto.NotaFiscal;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface INotaFiscalInterface
    {
        Task<ServiceResponse<List<NotaFiscalModel>>> BuscarTodasNotasFiscais();
        Task<ServiceResponse<NotaFiscalModel>> BuscarPorId(int id);
        Task<ServiceResponse<NotaFiscalModel>> CriarNotaFiscal(NotaFiscalCriacaoDto notaFiscalCriacaoDto);
        Task<ServiceResponse<NotaFiscalModel>> AtualizarNotaFiscal(NotaFiscalEdicaoDto notaFiscalEdicaoDto);
        Task<ServiceResponse<NotaFiscalModel>> DeletarNotaFiscal(int id);
    }
}
