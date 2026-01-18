namespace FakeDataGen.Application.UseCases.Records.Cnpj;

public sealed record GenerateCnpjInput(int Quantity, bool WithPunctuation, bool Alphanumeric);
