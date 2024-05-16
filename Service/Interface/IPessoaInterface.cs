using PharmaStock___API.Dto.Pessoa;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IPessoaInterface
    {
        Task<ServiceResponse<List<PessoaModel>>> BuscarTodasPessoas();
        Task<ServiceResponse<PessoaModel>> BuscarPorId(int id);
        Task<ServiceResponse<PessoaModel>> CriarPessoa(PessoaCriacaoDto pessoaCriacaoDto);
        Task<ServiceResponse<PessoaModel>> AtualizarPessoa(PessoaEdicaoDto pessoaEdicaoDto);
        Task<ServiceResponse<PessoaModel>> DeletarPessoa(int id);
    }
}
