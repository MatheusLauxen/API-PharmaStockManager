using PharmaStock___API.Dto.Auth;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;

namespace PharmaStock___API.Service.Interface
{
    public interface IAuthInterface
    {
        Task<ServiceResponse<UsuarioModel>> CreateAccount(AuthCriacaoDto authCriacaoDto);
        Task<ServiceResponse<string>> authenticateUser(AuthEntrar authEntrar);
        string GerarToken(UsuarioModel usuarioModel);
    }
}
