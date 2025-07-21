using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Repositories;
using Trichechus.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using Trichechus.Application.Interfaces;
using Trichechus.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Trichechus.Application.Common;
using Trichechus.API.Middlewares;
using Trichechus.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		policy => policy
			.WithOrigins("http://localhost:5116","http://10.0.20.34:5116") // Porta do seu frontend

			// .AllowAnyOrigin() //PARA DEV
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials() // Importante para credenciais
			);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

// Add DbContext with SQLite
builder.Services.AddDbContext<TrichechusDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Repositórios
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IBaseDadosRepository, BaseDadosRepository>();
builder.Services.AddScoped<ICatalogoRepository, CatalogoRepository>();
builder.Services.AddScoped<IContratoRepository, ContratoRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IFuncionalidadeRepository, FuncionalidadeRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
builder.Services.AddScoped<IRepositorioRepository, RepositorioRepository>();
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IURLRepository, URLRepository>();
// Registrar Repositórios Locais
builder.Services.AddScoped<IUsuarioLocalRepository, UsuarioLocalRepository>();

// Registrar Serviços
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IBaseDadosService, BaseDadosService>();
builder.Services.AddScoped<ICatalogoService, CatalogoService>();
builder.Services.AddScoped<IContratoService, ContratoService>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IFuncionalidadeService, FuncionalidadeService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();
builder.Services.AddScoped<IRepositorioService, RepositorioService>();
builder.Services.AddScoped<ISoftwareService, SoftwareService>();
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<IURLService, URLService>();
builder.Services.AddScoped<ILocalAutenticacaoService, LocalAutenticacaoService>();
builder.Services.AddScoped<ISGAAutenticacaoService, SGAAutenticacaoService>(); // Serviço de autenticação SGA
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ISGAClient, SGAClient>();

builder.Services.AddHttpClient<ISGAClient, SGAClient>();

// Registrar Contexto do Usuário
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

// Configurar FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Trichechus.Application.Validators.CreateAtividadeDtoValidator>();
// builder.Services.AddValidatorsFromAssemblyContaining<Trichechus.Application.Validators.CreateCatalogoDtoValidator>();

// Limpar mapeamentos de claims padrão (antes de AddAuthentication/AddJwtBearer)
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Configurar autenticação JWT
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	var configuration = builder.Configuration;

	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true, // <-- importante para validar expiração
		ValidateIssuerSigningKey = true,
		ValidIssuer = configuration["JWT:Issuer"],
		ValidAudience = configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)
		),
		//RoleClaimType = ClaimTypes.Role, // <- isso força o interpretador a usar "role"
		// RoleClaimType = "role", // <- isso força o interpretador a usar "role"

		// Tolerância para pequenas diferenças de tempo entre cliente e servidor
		ClockSkew = TimeSpan.FromMinutes(1) // ou 2-5 minutos se preferir
	};
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	// Adiciona suporte para autenticação JWT
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});

	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Trichechus API",
		Version = "v1",
		Description = "API para gerenciamento de atividades e tarefas do Sistema Trichechus - " + builder.Configuration.GetValue<string>("AMBIENTE")!.ToString(),
		Contact = new OpenApiContact
		{
			Name = "Luis Felipe Rocha Guimarães",
			Email = "lguimaraes@banparanet.com.br"
		}
	});

	// Habilitar anotações do Swagger
	c.EnableAnnotations();

	// Adiciona comentários XML (opcional)
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	if (File.Exists(xmlPath))
	{
		c.IncludeXmlComments(xmlPath);
	}
});

var app = builder.Build();

if (app.Environment.IsProduction())
{
	app.UsePathBase("/trichechus");
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/trichechus/swagger/v1/swagger.json", "Trichechus API v1");
		c.RoutePrefix = string.Empty; // Para servir a UI do Swagger na raiz
		c.DocExpansion(DocExpansion.None);
		c.DefaultModelsExpandDepth(-1); // Oculta a seção de modelos
		c.DisplayRequestDuration();
		c.EnableDeepLinking();
		c.EnableFilter();
		c.ShowExtensions();

		// Personalização de cores e estilos
		c.InjectStylesheet("/trichechus/swagger-ui/custom.css");
		c.InjectJavascript("/trichechus/swagger-ui/custom.js");
	});
}

// Swagger & Middlewares
if (app.Environment.IsDevelopment())
{
	IdentityModelEventSource.ShowPII = true;
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trichechus API v1");
		c.RoutePrefix = string.Empty; // Para servir a UI do Swagger na raiz
		c.DocExpansion(DocExpansion.None); //Fecha todos os endpoints
		c.DefaultModelsExpandDepth(-1); // Oculta a seção de modelos
		c.DisplayRequestDuration();
		c.EnableDeepLinking();
		c.EnableFilter();
		c.ShowExtensions();

		// Personalização de cores e estilos
		c.InjectStylesheet("/swagger-ui/custom.css");
		c.InjectJavascript("/swagger-ui/custom.js");
	});
}

// Adicionar o middleware CORS - IMPORTANTE: Deve vir antes de UseRouting, UseAuthentication, UseAuthorization e MapControllers
app.UseCors("AllowFrontend");

// Adicionar middleware de tratamento de exceções
app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting(); // Middleware de roteamento
app.UseAuthentication(); // Middleware de autenticação <-- TEM que vir antes de UseAuthorization
app.UseAuthorization(); // Middleware de autorização

// Adicionar middleware de contexto do usuário (após autenticação/autorização)
app.UseMiddleware<UserContextMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
