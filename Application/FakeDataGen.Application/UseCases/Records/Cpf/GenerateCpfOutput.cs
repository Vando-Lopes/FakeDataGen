namespace FakeDataGen.Application.UseCases.Records.Cpf;

public sealed record GenerateCpfOutput(
    IReadOnlyList<string> Cpfs
);
