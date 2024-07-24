using Agendamento.Infra.IoC;
using Microsoft.OpenApi.Models;
using Agendamento.WebAPI.Middleware;
using Agendamento.WebAPI.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Carregar configuração do appsettings.json
var configuration = builder.Configuration;

// Obter o caminho do arquivo de credenciais do Google
var googleCredentialsPath = configuration["Google:ApplicationCredentials"];

// Definir a variável de ambiente
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", googleCredentialsPath);

// Adicionar serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.OperationFilter<FileUploadOperation>(); 
});
builder.Services.AddInfrastructures(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();