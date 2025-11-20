using Finan.Application;
using Finan.Application.Middlewares;
using Finan.Service.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

//
// Plano (pseudocódigo detalhado):
// 1. Ler a variável de ambiente ASPNETCORE_ENVIRONMENT (ou fallback para "Production").
// 2. Construir um ConfigurationBuilder manualmente:
//    - SetBasePath para o diretório atual.
//    - Adicionar "appsettings.json" (obrigatório).
//    - Adicionar "appsettings.{environment}.json" (opcional).
//    - Adicionar variáveis de ambiente.
//    - Adicionar argumentos de linha de comando.
// 3. Se ambiente for "Development", opcionalmente habilitar User Secrets (se aplicável).
// 4. Criar o WebApplicationBuilder usando WebApplicationOptions definindo EnvironmentName.
// 5. Mesclar a configuração construída manualmente com o builder.Configuration.
// 6. Continuar com o restante da configuração (serviços, autenticação, middlewares).
//

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

// 1-3: Construir configuração baseada no ambiente
var externalConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

// 4: Criar o builder com EnvironmentName explícito para garantir consistência
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = environment
});

// 5: Mesclar a configuração personalizada (preserva fontes já registradas)
builder.Configuration.AddConfiguration(externalConfig);

// Habilitar User Secrets automaticamente em Development (opcional, requer Package Microsoft.Extensions.Configuration.UserSecrets)
// if (builder.Environment.IsDevelopment())
// {
//     builder.Configuration.AddUserSecrets<Program>(optional: true);
// }

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt")
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Finan.Api",
        Description = "Api de Financeiro"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo de autorização. Exemplo: {Bearer + Token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<String>()
                }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
    };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("https://seu-frontend.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Middleware global de tratamento de exceções
app.UseMiddleware<ExceptionMiddleware>();

app.Services.AddMigrations();

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

app.UseCors("CorsPolicy");
app.UseCors("AllowFrontend");

app.Run();
