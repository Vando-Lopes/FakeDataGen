using FakeDataGen.Api.Contracts;
using FakeDataGen.Application.UseCases;
using FakeDataGen.Application.UseCases.Records.Cpf;

namespace FakeDataGen.Api.Endpoints;

public static class CpfEndpoints
{
    public static void MapCpfEndpoints(this WebApplication app)
    {
        app.MapGet("/api/v1/cpf", (GenerateCpfUseCase useCase, int quantity = 1, bool punctuation = true) =>
        {
            var input = new GenerateCpfInput(quantity, punctuation);
            var result = useCase.Execute(input);

            return Results.Ok(result);
        })
        .WithTags("CPF")
        .WithName("GenerateCpf")
        .WithDescription("Gera CPFs válidos de forma determinística para uso em testes.")
        .Produces<IEnumerable<string>>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            var quantity = operation?.Parameters?.FirstOrDefault(p => p.Name == "quantity");
            quantity?.Description = "Quantidade de CPFs a serem gerados. Valor mínimo: 1. Valor máximo: 100. Padrão: 1.";

            var punctuation = operation?.Parameters?.FirstOrDefault(p => p.Name == "punctuation");
            punctuation?.Description = "Define se o CPF deve conter pontuação (ex: 123.456.789-00).";

            return Task.CompletedTask;
        });
    }
}
