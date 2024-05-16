using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.Medico;
using PharmaStock___API.Dto.Paciente;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class PacienteService : IPacienteInterface
    {
        private readonly BancoContext _bancoContext;

        public PacienteService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<PacienteModel>>> BuscarTodosPacientes()
        {
            ServiceResponse<List<PacienteModel>> serviceResponse = new ServiceResponse<List<PacienteModel>>();

            try
            {
                var pacientes = await _bancoContext.Paciente.Include(x => x.Pessoa).ToListAsync();

                serviceResponse.dados = pacientes;
                serviceResponse.mensagem = "Todos os pacientes foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PacienteModel>> BuscarPorId(int id)
        {
            ServiceResponse<PacienteModel> serviceResponse = new ServiceResponse<PacienteModel>();

            try
            {
                var pacientes = await _bancoContext.Paciente.Include(x => x.Pessoa).FirstOrDefaultAsync(x => x.id == id);

                if (pacientes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = pacientes;
                serviceResponse.mensagem = "Paciente encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PacienteModel>> AdicionarPaciente(PacienteCriacaoDto pacienteCriacaoDto)
        {
            ServiceResponse<PacienteModel> serviceResponse = new ServiceResponse<PacienteModel>();

            try
            {
                var pacientes = new PacienteModel()
                {
                    idPessoa = pacienteCriacaoDto.idPessoa,
                    cartaoSus = pacienteCriacaoDto.cartaoSus,
                    planoSaude = pacienteCriacaoDto.planoSaude
                };

                _bancoContext.Add(pacientes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = pacientes;
                serviceResponse.mensagem = "Paciente cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PacienteModel>> AtualizarPaciente(PacienteModel pacienteModel)
        {
            ServiceResponse<PacienteModel> serviceResponse = new ServiceResponse<PacienteModel>();

            try
            {
                var pacientes = await _bancoContext.Paciente.FirstOrDefaultAsync(x => x.id == pacienteModel.id);

                if (pacientes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                pacientes.idPessoa = pacienteModel.idPessoa;
                pacientes.cartaoSus = pacienteModel.cartaoSus;
                pacientes.planoSaude = pacienteModel.planoSaude;

                _bancoContext.Update(pacientes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = pacientes;
                serviceResponse.mensagem = "Paciente atualizado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<PacienteModel>> DeletarPaciente(int id)
        {
            ServiceResponse<PacienteModel> serviceResponse = new ServiceResponse<PacienteModel>();

            try
            {
                var pacientes = await _bancoContext.Paciente.FirstOrDefaultAsync(x => x.id == id);

                if (pacientes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                var pacienteNaTabelaRegistroMovimento = _bancoContext.RegistroMovimento.FirstOrDefault(me => me.idPaciente == id);

                if (pacienteNaTabelaRegistroMovimento != null)
                {
                    serviceResponse.mensagem = "O paciente selecionado não pode ser excluído porque está em uso em outra tabela.";
                    return serviceResponse;
                }

                _bancoContext.Paciente.Remove(pacientes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = pacientes;
                serviceResponse.mensagem = "Paciente excluído com sucesso!";

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
