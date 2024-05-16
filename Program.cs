using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PharmaStock___API.Data;
using PharmaStock___API.Service;
using PharmaStock___API.Service.Interface;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BancoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IFornecedorInterface, FornecedorService>();
builder.Services.AddScoped<INotaFiscalInterface, NotaFiscalService>();
builder.Services.AddScoped<IProdutoInterface, ProdutoService>();
builder.Services.AddScoped<IEstoqueInterface, EstoqueService>();
builder.Services.AddScoped<IPessoaInterface, PessoaService>();
builder.Services.AddScoped<IMedicoInterface, MedicoService>();
builder.Services.AddScoped<IPacienteInterface, PacienteService>();
builder.Services.AddScoped<IPermissaoUsuarioInterface, PermissaoUsuarioService>();
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IRegistroMovimentoInterface, RegistroMovimentoService>();
builder.Services.AddScoped<IProdutoNotaInterface, ProdutoNotaService>();

var tokenKey = builder.Configuration["AppSettings:Token"];

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standar Authorization header using the Bearer scheme (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
