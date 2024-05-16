using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Auth;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PharmaStock___API.Service
{
    public class AuthService : IAuthInterface
    {
        private readonly BancoContext _bancoContext;
        private IConfiguration _config;

        public AuthService(BancoContext bancoContext, IConfiguration config)
        {
            _bancoContext = bancoContext;
            _config = config;
        }

        public async Task<ServiceResponse<UsuarioModel>> CreateAccount(AuthCriacaoDto authCriacaoDto)
        {
            ServiceResponse<UsuarioModel> serviceResponse = new ServiceResponse<UsuarioModel>();

            try
            {
                var usuarioExistente = await _bancoContext.Usuario.FirstOrDefaultAsync(u => u.login == authCriacaoDto.login);

                if (usuarioExistente != null)
                {
                    serviceResponse.mensagem = "Já existe um usuário com esse mesmo login registrado na base de dados!";
                    serviceResponse.sucesso = false;

                    return serviceResponse;
                }

                string senhaCriptografada = EncryptPassword.Encode(authCriacaoDto.senha);

                var usuario = new UsuarioModel()
                {
                    idPessoa = authCriacaoDto.idPessoa,
                    login = authCriacaoDto.login,
                    senha = senhaCriptografada,
                    idPermissao = authCriacaoDto.idPermissao
                };

                _bancoContext.Add(usuario);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = usuario;
                serviceResponse.mensagem = "Usuário cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<string>> authenticateUser(AuthEntrar authEntrar)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                var usuario = await _bancoContext.Usuario.Include(x => x.PermissaoUsuario).FirstOrDefaultAsync(x => x.login == authEntrar.login);

                if (usuario == null)
                {
                    serviceResponse.mensagem = "Usuário não cadastrado na base de dados!";
                    serviceResponse.sucesso = false;

                    return serviceResponse;
                }

                string senhaCriptografada = EncryptPassword.Encode(authEntrar.senha);

                if (senhaCriptografada != null && usuario.senha == senhaCriptografada)
                {
                    var token = GerarToken(usuario);

                    serviceResponse.dados = token;
                    serviceResponse.mensagem = $"Usuário logado com sucesso!";
                }
                else
                {
                    serviceResponse.mensagem = "Senha incorreta!";
                    serviceResponse.sucesso = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
            }

            return serviceResponse;
        }

        public string GerarToken(UsuarioModel usuarioModel)
        {
            var tokenKey = _config["AppSettings:Token"];

            var claims = new List<Claim>
            {
                new Claim("idPessoa", usuarioModel.idPessoa.ToString()),
                new Claim("login", usuarioModel.login),
                new Claim("Permissao", usuarioModel.PermissaoUsuario.descricao)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
