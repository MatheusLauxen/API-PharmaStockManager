using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Medico;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class MedicoService : IMedicoInterface
    {
        private readonly BancoContext _bancoContext;

        public MedicoService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<MedicoModel>>> BuscarTodosMedicos()
        {
            ServiceResponse<List<MedicoModel>> serviceResponse = new ServiceResponse<List<MedicoModel>>();

            try
            {
                var medicos = await _bancoContext.Medico.Include(x => x.Pessoa).ToListAsync();

                serviceResponse.dados = medicos;
                serviceResponse.mensagem = "Todos os medicos foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<MedicoModel>> BuscarPorId(int id)
        {
            ServiceResponse<MedicoModel> serviceResponse = new ServiceResponse<MedicoModel>();

            try
            {
                var medicos = await _bancoContext.Medico.Include(x => x.Pessoa).FirstOrDefaultAsync(x => x.id == id);

                if (medicos == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = medicos;
                serviceResponse.mensagem = "Médico encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<MedicoModel>> AdicionarMedico(MedicoCriacaoDto medicoCriacaoDto)
        {
            ServiceResponse<MedicoModel> serviceResponse = new ServiceResponse<MedicoModel>();

            try
            {
                var medicos = new MedicoModel()
                {
                    idPessoa = medicoCriacaoDto.idPessoa,
                    crm = medicoCriacaoDto.crm
                };

                _bancoContext.Add(medicos);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = medicos;
                serviceResponse.mensagem = "Médico cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<MedicoModel>> AtualizarMedico(MedicoModel medicoModel)
        {
            ServiceResponse<MedicoModel> serviceResponse = new ServiceResponse<MedicoModel>();

            try
            {
                var medicos = await _bancoContext.Medico.FirstOrDefaultAsync(x => x.id == medicoModel.id);

                if (medicos == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                medicos.idPessoa = medicoModel.idPessoa;
                medicos.crm = medicoModel.crm;

                _bancoContext.Update(medicos);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = medicos;
                serviceResponse.mensagem = "Médico atualizado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<MedicoModel>> DeletarMedico(int id)
        {
            ServiceResponse<MedicoModel> serviceResponse = new ServiceResponse<MedicoModel>();

            try
            {
                var medicos = await _bancoContext.Medico.FirstOrDefaultAsync(x => x.id == id);

                if (medicos == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                var medicoNaTabelaRegistroMovimento = _bancoContext.RegistroMovimento.FirstOrDefault(me => me.idMedico == id);

                if (medicoNaTabelaRegistroMovimento != null)
                {
                    serviceResponse.mensagem = "O médico selecionado não pode ser excluído porque está em uso em outra tabela.";
                    return serviceResponse;
                }

                _bancoContext.Medico.Remove(medicos);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = medicos;
                serviceResponse.mensagem = "Médico excluído com sucesso!";

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
