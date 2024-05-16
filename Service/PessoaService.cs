using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Pessoa;
using PharmaStock___API.Dto.Produto;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class PessoaService : IPessoaInterface
    {
        private readonly BancoContext _bancoContext;

        public PessoaService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<PessoaModel>>> BuscarTodasPessoas()
        {
            ServiceResponse<List<PessoaModel>> serviceResponse = new ServiceResponse<List<PessoaModel>>();

            try
            {
                var pessoas = await _bancoContext.Pessoa.ToListAsync();

                serviceResponse.dados = pessoas;
                serviceResponse.mensagem = "Todas as pessoas foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PessoaModel>> BuscarPorId(int id)
        {
            ServiceResponse<PessoaModel> serviceResponse = new ServiceResponse<PessoaModel>();

            try
            {
                var pessoas = await _bancoContext.Pessoa.FirstOrDefaultAsync(x => x.id == id);

                if (pessoas == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = pessoas;
                serviceResponse.mensagem = "Pessoa encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PessoaModel>> CriarPessoa(PessoaCriacaoDto pessoaCriacaoDto)
        {
            ServiceResponse<PessoaModel> serviceResponse = new ServiceResponse<PessoaModel>();

            try
            {
                var pessoas = new PessoaModel()
                {
                    nome = pessoaCriacaoDto.nome,
                    dtaNascimento = pessoaCriacaoDto.dtaNascimento,
                    cpf = pessoaCriacaoDto.cpf,
                    telefone = pessoaCriacaoDto.telefone,
                    sexo = pessoaCriacaoDto.sexo,

                    dtaCadastro = DateTime.Now
                };

                _bancoContext.Add(pessoas);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = pessoas;
                serviceResponse.mensagem = "Pessoa cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PessoaModel>> AtualizarPessoa(PessoaEdicaoDto pessoaEdicaoDto)
        {
            ServiceResponse<PessoaModel> serviceResponse = new ServiceResponse<PessoaModel>();

            try
            {
                var pessoas = await _bancoContext.Pessoa.FirstOrDefaultAsync(x => x.id == pessoaEdicaoDto.id);

                if (pessoas == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                pessoas.nome = pessoaEdicaoDto.nome;
                pessoas.dtaNascimento = pessoaEdicaoDto.dtaNascimento;
                pessoas.cpf = pessoaEdicaoDto.cpf;
                pessoas.telefone = pessoaEdicaoDto.telefone;
                pessoas.sexo = pessoaEdicaoDto.sexo;

                _bancoContext.Update(pessoas);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = pessoas;
                serviceResponse.mensagem = "Pessoa atualizado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PessoaModel>> DeletarPessoa(int id)
        {
            ServiceResponse<PessoaModel> serviceResponse = new ServiceResponse<PessoaModel>();

            try
            {
                var pessoas = await _bancoContext.Pessoa.FirstOrDefaultAsync(x => x.id == id);

                if (pessoas == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                var pessoaNaTabelaMedico = _bancoContext.Medico.FirstOrDefault(me => me.idPessoa == id);
                var pessoaNaTabelaPaciente = _bancoContext.Paciente.FirstOrDefault(me => me.idPessoa == id);
                var pessoaNaTabelaUsuario = _bancoContext.Usuario.FirstOrDefault(me => me.idPessoa == id);

                if (pessoaNaTabelaMedico != null || pessoaNaTabelaPaciente != null || pessoaNaTabelaUsuario != null)
                {
                    serviceResponse.mensagem = "A pessoa selecionada não pode ser excluída porque está em uso em outra tabela.";
                    return serviceResponse;
                }


                _bancoContext.Pessoa.Remove(pessoas);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = pessoas;
                serviceResponse.mensagem = "Pessoa excluído com sucesso!";

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
