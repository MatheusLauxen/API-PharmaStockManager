using Microsoft.EntityFrameworkCore;
using PharmaStock___API.Models;

namespace PharmaStock___API.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<FornecedorModel> Fornecedor { get; set; }
        public DbSet<NotaFiscalModel> NotaFiscal { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<EstoqueModel> Estoque { get; set; }
        public DbSet<PessoaModel> Pessoa { get; set; }
        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<PermissaoUsuarioModel> PermissaoUsuario { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<RegistroMovimentoModel> RegistroMovimento { get; set; }
        public DbSet<ProdutoNotaModel> ProdutoNota { get; set; }
    }
}
