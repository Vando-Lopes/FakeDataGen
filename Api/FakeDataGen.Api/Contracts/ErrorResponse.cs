namespace FakeDataGen.Api.Contracts;

public sealed record ErrorResponse(
    string Message,
    string? Detail = null
);
