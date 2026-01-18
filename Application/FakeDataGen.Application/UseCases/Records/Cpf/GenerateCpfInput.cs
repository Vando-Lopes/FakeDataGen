namespace FakeDataGen.Application.UseCases.Records.Cpf;

public sealed record GenerateCpfInput(
    int Quantity,
    bool WithPunctuation
);
