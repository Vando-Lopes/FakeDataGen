using FakeDataGen.Api.Contracts;

namespace FakeDataGen.Api.Errors;

public static class ErrorDocumentationEndpoints
{
    public static void MapErrorDocumentation(this WebApplication app)
    {
        app.MapGet("/errors", () =>
        {
            return Results.Ok();
        })
        .WithTags("Errors")
        .WithName("ErrorsDocumentation")
        .WithSummary("Padrão de erros da API")
        .WithDescription("""
        Esta API utiliza um formato padrão para retorno de erros.

        ## Formato da resposta de erro

        Todas as respostas de erro seguem o seguinte formato JSON:

        ```json
        {
          "message": "Descrição legível do erro",
          "detail": "Detalhe técnico opcional"
        }
        ```

        ## Erro de validação (400)

        Retornado quando os dados da requisição são inválidos.

        Exemplo:
        ```json
        {
          "message": "A quantidade deve estar entre 1 e 100"
        }
        ```

        ## Erro interno (500)

        Retornado quando ocorre um erro inesperado no servidor.

        Exemplo:
        ```json
        {
          "message": "Ocorreu um erro inesperado.",
          "detail": "Detalhe técnico do erro"
        }
        ```
        """)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
