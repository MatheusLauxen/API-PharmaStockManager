using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Data;
using PharmaStock___API.Dto.RegistroMovimento;
using PharmaStock___API.Helpers;
using PharmaStock___API.Models;
using PharmaStock___API.Service.Interface;

namespace PharmaStock___API.Service
{
    public class RegistroMovimentoService : IRegistroMovimentoInterface
    {
        private readonly BancoContext _bancoContext;

        public RegistroMovimentoService(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ServiceResponse<List<RegistroMovimentoModel>>> BuscarTodasMovimentacoes()
        {
            ServiceResponse<List<RegistroMovimentoModel>> serviceResponse = new ServiceResponse<List<RegistroMovimentoModel>>();

            try
            {
                var movimentacoes = await _bancoContext.RegistroMovimento.Include(x => x.Usuario).Include(x => x.Medico).Include(x => x.Produto).Include(x => x.Paciente).ToListAsync();

                serviceResponse.dados = movimentacoes;
                serviceResponse.mensagem = "Todas movimentações foram encontrados!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<RegistroMovimentoModel>> BuscarPorId(int id)
        {
            ServiceResponse<RegistroMovimentoModel> serviceResponse = new ServiceResponse<RegistroMovimentoModel>();

            try
            {
                var movimentacoes = await _bancoContext.RegistroMovimento.Include(x => x.Usuario).Include(x => x.Medico).Include(x => x.Produto).Include(x => x.Paciente).FirstOrDefaultAsync(x => x.id == id);

                if (movimentacoes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                serviceResponse.dados = movimentacoes;
                serviceResponse.mensagem = $"Movimentação do ID {movimentacoes.id} encontrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<RegistroMovimentoModel>> CriarMovimentacao(RegistroMovimentoCriacaoDto registroMovimentoCriacaoDto)
        {
            ServiceResponse<RegistroMovimentoModel> serviceResponse = new ServiceResponse<RegistroMovimentoModel>();

            try
            {
                var produtoNoEstoque = await _bancoContext.Estoque.FindAsync(registroMovimentoCriacaoDto.idProduto);

                if (produtoNoEstoque == null)
                {
                    serviceResponse.mensagem = "O produto não foi encontrado no estoque.";
                    serviceResponse.sucesso = false;
                    return serviceResponse;
                }

                if (registroMovimentoCriacaoDto.tipoTransacao == "S")
                {
                    if (produtoNoEstoque.quantidade < registroMovimentoCriacaoDto.quantidade)
                    {
                        serviceResponse.mensagem = $"Quantidade insuficiente do produto no estoque. Saldo atual é de {produtoNoEstoque.quantidade}";
                        serviceResponse.sucesso = false;
                        return serviceResponse;
                    }

                    produtoNoEstoque.quantidade -= registroMovimentoCriacaoDto.quantidade;
                }

                if (registroMovimentoCriacaoDto.tipoTransacao == "E")
                {
                    produtoNoEstoque.quantidade += registroMovimentoCriacaoDto.quantidade;
                }

                _bancoContext.Update(produtoNoEstoque);
                await _bancoContext.SaveChangesAsync();


                var movimentacoes = new RegistroMovimentoModel()
                {
                    idUsuario = registroMovimentoCriacaoDto.idUsuario,
                    idMedico = registroMovimentoCriacaoDto.idMedico,
                    dtaHora = registroMovimentoCriacaoDto.dtaHora,
                    tipoTransacao = registroMovimentoCriacaoDto.tipoTransacao,
                    idProduto = registroMovimentoCriacaoDto.idProduto,
                    quantidade = registroMovimentoCriacaoDto.quantidade,
                    idPaciente = registroMovimentoCriacaoDto.idPaciente
                };

                _bancoContext.Add(movimentacoes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = movimentacoes;
                serviceResponse.mensagem = "Movimentação cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<RegistroMovimentoModel>> AtualizarMovimentacao(RegistroMovimentoModel registroMovimentoModel)
        {
            ServiceResponse<RegistroMovimentoModel> serviceResponse = new ServiceResponse<RegistroMovimentoModel>();

            try
            {
                var movimentacoes = await _bancoContext.RegistroMovimento.FirstOrDefaultAsync(x => x.id == registroMovimentoModel.id);

                if (movimentacoes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }

                movimentacoes.idUsuario = registroMovimentoModel.idUsuario;
                movimentacoes.idMedico = registroMovimentoModel.idMedico;
                movimentacoes.dtaHora = registroMovimentoModel.dtaHora;
                movimentacoes.tipoTransacao = registroMovimentoModel.tipoTransacao;
                movimentacoes.idProduto = registroMovimentoModel.idProduto;
                movimentacoes.quantidade = registroMovimentoModel.quantidade;
                movimentacoes.idPaciente = registroMovimentoModel.idPaciente;

                _bancoContext.Update(movimentacoes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = movimentacoes;
                serviceResponse.mensagem = "Movimentação atualizada com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<RegistroMovimentoModel>> DeletarMovimentacao(int id)
        {
            ServiceResponse<RegistroMovimentoModel> serviceResponse = new ServiceResponse<RegistroMovimentoModel>();

            try
            {
                var movimentacoes = await _bancoContext.RegistroMovimento.FirstOrDefaultAsync(x => x.id == id);

                if (movimentacoes == null)
                {
                    serviceResponse.mensagem = "Nenhum registro encontrado. Verificar o ID informado!";
                    return serviceResponse;
                }        

                _bancoContext.RegistroMovimento.Remove(movimentacoes);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = movimentacoes;
                serviceResponse.mensagem = "Movimentação excluído com sucesso!";

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
