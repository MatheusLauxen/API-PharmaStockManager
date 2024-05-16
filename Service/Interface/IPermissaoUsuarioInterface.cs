using PharmaStock___API.Dto.PermissaoUsuario;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IPermissaoUsuarioInterface
    {
        Task<ServiceResponse<List<PermissaoUsuarioModel>>> BuscarTodasPermissoes();
        Task<ServiceResponse<PermissaoUsuarioModel>> BuscarPorId(int id);
        Task<ServiceResponse<PermissaoUsuarioModel>> CriarPermissao(PermissaoUsuarioCriacaoDto permissaoUsuarioCriacaoDto);
        Task<ServiceResponse<PermissaoUsuarioModel>> AtualizarPermissao(PermissaoUsuarioModel permissaoUsuarioModel);
        Task<ServiceResponse<PermissaoUsuarioModel>> DeletarPermissao(int id);
    }
}
