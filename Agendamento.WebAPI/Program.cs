using Agendamento.Infra.IoC;
using Microsoft.OpenApi.Models;
using Agendamento.WebAPI.Middleware;
using Agendamento.WebAPI.Swagger;
using Agendamento.WebAPI.Configurations; // Importando a configuração de CORS

var builder = WebApplication.CreateBuilder(args);

// Adicionar configuração de CORS
builder.Services.AddCorsPolicy();

// Carregar configuração do appsettings.json
var configuration = builder.Configuration;

// Configuração do Google Cloud (deixada como estava)
var googleCredentialsPath = configuration["Google:ApplicationCredentials"];
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

app.UseCors("AllowSpecificOrigins"); 

app.UseAuthorization();

app.MapControllers();

app.Run();