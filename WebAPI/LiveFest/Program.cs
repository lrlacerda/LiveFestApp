
using LiveFest.Context;
using LiveFest.Utils.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner.
// Configura a conex�o com o banco de dados.
builder.Services.AddDbContext<LiveFestContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDataBase"));
});

builder.Services.AddControllers();

// Configurar o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API LiveFest",
        Description = "Backend API",
        Contact = new OpenApiContact
        {
            Name = "LiveFestApp"
        }
    });
});

// Configurar EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));

// Registrar o servi�o de envio de e-mails
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<EmailSendingService>();

////Adiciona servi�o Jwt Bearer (forma de autentica��o)
////Deixar indentado assim:
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultChallengeScheme = "JwtBearer";
//    options.DefaultAuthenticateScheme = "JwtBearer";
//})
////Deixar indentado assim:
//.AddJwtBearer("JwtBearer", options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        //Valida quem esta solicitando
//        ValidateIssuer = true,
//        //Valida quem esta recebendo
//        ValidateAudience = true,
//        //Define se o tempo de expira��o ser� validado
//        ValidateLifetime = true,
//        //Forma de criptografia que valida a chave de autentifica��o
//        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autenticacao-webapi-eventos-livefest")),
//        //Valida o tempo de expira��o do token ClockSkew = TimeSpan.FromMinutes(5),
//        //Nome do issuer (de onde esta vindo) ValidIssuer = "webapi.Filmes",
//        //Nome do issuer (para onde esta indo) ValidAudience = "webapi.Filmes"
//    };
//});

var app = builder.Build();

// Configurar o pipeline de solicita��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
