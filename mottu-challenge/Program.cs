using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using mottu_challenge.Connection;
using mottu_challenge.Mappers;
using mottu_challenge.Repository;
using mottu_challenge.Services;
using System.Reflection;
using System.Text; // Para o Swagger

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration; // Para ler o appsettings


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu Challenge API",
        Version = "v1",
        Description = "API RESTful para o desafio Mottu, com gerenciamento de usuários, perfis e motos."
    });

    // Adiciona definição de segurança (Bearer)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // Usando Http (como no exemplo do professor)
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira 'Bearer' [espaço] e seu token JWT no campo abaixo."
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
            new string[] {}
        }
    });

    // Leitura dos comentários XML (do seu código original)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// --- 2. Seus Serviços Existentes ---
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(Configuration.GetConnectionString("DefaultConnection")));

// --- 3. Serviços de Autenticação (do seu professor) ---

// Adiciona o TokenService
builder.Services.AddScoped<TokenService>(); // Usar Scoped é melhor que Singleton para serviços

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Adiciona Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),

            ValidateIssuer = true,
            ValidIssuer = Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddApiVersioning(options =>
{
    // Reporta as versões suportadas no header da resposta
    options.ReportApiVersions = true;

    // Assume a versão padrão (1.0) se o cliente não especificar
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);

    // Método de leitura (vamos usar pela URL, ex: /api/v1/...)
    options.ApiVersionReader = new UrlSegmentApiVersionReader();

}).AddApiExplorer(options =>
{
    // Formato do nome do grupo no Swagger (ex: 'v1')
    options.GroupNameFormat = "'v'VVV";

    // Substitui {version} na rota pelo número da versão
    options.SubstituteApiVersionInUrl = true;
});

// Adiciona Autorização
builder.Services.AddAuthorization();

builder.Services.AddHealthChecks()
    .AddOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        name: "OracleDB-Check",
        failureStatus: HealthStatus.Unhealthy, // O que reportar se falhar
        tags: new[] { "database", "ready" }
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API V1");
    });
}

app.UseHttpsRedirection();

// --- 4. Pipeline de Autenticação (do seu professor) ---
app.UseAuthentication(); // <-- Muito importante
app.UseAuthorization();  // <-- Muito importante

app.MapHealthChecks("/health");
app.MapControllers();
app.Run();