using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.PermissaoUsuario;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class PermissaoUsuarioService : IPermissaoUsuarioInterface
    {
        private readonly BancoContext _bancoContext;

        public PermissaoUsuarioService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<PermissaoUsuarioModel>>> BuscarTodasPermissoes()
        {
            ServiceResponse<List<PermissaoUsuarioModel>> serviceResponse = new ServiceResponse<List<PermissaoUsuarioModel>>();

            try
            {
                var permissoes = await _bancoContext.PermissaoUsuario.ToListAsync();

                serviceResponse.dados = permissoes;
                serviceResponse.mensagem = "Todas as permissões foram encontradas!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PermissaoUsuarioModel>> BuscarPorId(int id)
        {
            ServiceResponse<PermissaoUsuarioModel> serviceResponse = new ServiceResponse<PermissaoUsuarioModel>();

            try
            {
                var permissoes = await _bancoContext.PermissaoUsuario.FirstOrDefaultAsync(x => x.id == id);

                if (permissoes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = permissoes;
                serviceResponse.mensagem = "Permissão encontrada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PermissaoUsuarioModel>> CriarPermissao(PermissaoUsuarioCriacaoDto permissaoUsuarioCriacaoDto)
        {
            ServiceResponse<PermissaoUsuarioModel> serviceResponse = new ServiceResponse<PermissaoUsuarioModel>();

            try
            {
                var permissoes = new PermissaoUsuarioModel()
                {
                    descricao = permissaoUsuarioCriacaoDto.descricao,
                };

                _bancoContext.Add(permissoes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = permissoes;
                serviceResponse.mensagem = "Permissão cadastrada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PermissaoUsuarioModel>> AtualizarPermissao(PermissaoUsuarioModel permissaoUsuarioModel)
        {
            ServiceResponse<PermissaoUsuarioModel> serviceResponse = new ServiceResponse<PermissaoUsuarioModel>();

            try
            {
                var permissoes = await _bancoContext.PermissaoUsuario.FirstOrDefaultAsync(x => x.id == permissaoUsuarioModel.id);

                if (permissoes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                permissoes.descricao= permissaoUsuarioModel.descricao;        

                _bancoContext.Update(permissoes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = permissoes;
                serviceResponse.mensagem = "Permissão atualizada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PermissaoUsuarioModel>> DeletarPermissao(int id)
        {
            ServiceResponse<PermissaoUsuarioModel> serviceResponse = new ServiceResponse<PermissaoUsuarioModel>();

            try
            {
                var permissoes = await _bancoContext.PermissaoUsuario.FirstOrDefaultAsync(x => x.id == id);

                if (permissoes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                var permissaoNaTabelaUsuario = _bancoContext.Usuario.FirstOrDefault(me => me.idPermissao == id);

                if (permissaoNaTabelaUsuario != null)
                {
                    serviceResponse.mensagem = "A permissão selecionada não pode ser excluída porque está em uso em outra tabela.";
                    return serviceResponse;
                }

                _bancoContext.PermissaoUsuario.Remove(permissoes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = permissoes;
                serviceResponse.mensagem = "Permissão excluído com sucesso!";

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
