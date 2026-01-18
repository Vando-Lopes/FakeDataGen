using FakeDataGen.Api.Endpoints;
using FakeDataGen.Api.Errors;
using FakeDataGen.Api.Middlewares;
using FakeDataGen.Application;
using FakeDataGen.Core;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedHost;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Info.Description = """
        API para geração de dados falsos para testes e desenvolvimento.

        Recursos disponíveis:
        - CPF
        - CNPJ
        - Nomes
        - Endereços
        """;

        doc.Tags ??= new HashSet<OpenApiTag>();

        var cpfTag = doc.Tags?.FirstOrDefault(t => t.Name == "CPF");

        // Se já existe, apenas atualizamos a descrição
        cpfTag?.Description = """
            Geração de CPFs válidos para testes e desenvolvimento.

            - Os CPFs gerados **não pertencem a pessoas reais**
            - Ideal para ambientes de **QA, testes automatizados e mocks**
            """;

        return Task.CompletedTask;
    });
});

builder.Services.AddCore();
builder.Services.AddApplication();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapGet("/", () => Results.Redirect("/scalar"));
app.MapOpenApi();
app.MapErrorDocumentation();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("FakeDataGen API")
        .WithTheme(ScalarTheme.Default);
});

app.MapCpfEndpoints();
app.MapCnpjEndpoints();
app.UseForwardedHeaders();

await app.RunAsync();
