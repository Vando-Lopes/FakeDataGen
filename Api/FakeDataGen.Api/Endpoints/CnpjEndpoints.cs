using FakeDataGen.Api.Contracts;
using FakeDataGen.Application.UseCases;
using FakeDataGen.Application.UseCases.Records.Cnpj;

namespace FakeDataGen.Api.Endpoints;

public static class CnpjEndpoints
{
    public static void MapCnpjEndpoints(this WebApplication app)
    {
        app.MapGet("/api/v1/cnpj", (GenerateCnpjUseCase useCase, int quantity = 1, bool punctuation = true, bool alphanumeric = false) =>
        {
            var input = new GenerateCnpjInput(quantity, punctuation, alphanumeric);
            var result = useCase.Execute(input);

            return Results.Ok(result);
        })
        .WithTags("CNPJ")
        .WithName("GenerateCnpj")
        .WithDescription("Gera Cnpj's válidos de forma determinística para uso em testes.")
        .Produces<IEnumerable<string>>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            var quantity = operation?.Parameters?.FirstOrDefault(p => p.Name == "quantity");
            quantity?.Description = "Quantidade de CNPJs a serem gerados. Valor mínimo: 1. Valor máximo: 100. Padrão: 1.";

            var punctuation = operation?.Parameters?.FirstOrDefault(p => p.Name == "punctuation");
            punctuation?.Description = "Define se o CNPJ deve conter pontuação (ex: 123.456.789-00).";

            return Task.CompletedTask;
        });
    }
}
