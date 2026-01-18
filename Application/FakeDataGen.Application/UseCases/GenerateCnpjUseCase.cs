using FakeDataGen.Application.Exceptions;
using FakeDataGen.Application.UseCases.Records.Cnpj;
using FakeDataGen.Core.Ports;

namespace FakeDataGen.Application.UseCases;

public sealed class GenerateCnpjUseCase(ICnpjGenerator generator)
{
    public GenerateCnpjOutput Execute(GenerateCnpjInput input)
    {
        if (input.Quantity <= 0 || input.Quantity > 100)
            throw new ValidationException("A quantidade deve estar entre 1 e 100");

        var cnpjs = new List<string>();

        for (int i = 0; i < input.Quantity; i++)
        {
            var cnpj = generator.Generate(input.Alphanumeric);
            cnpjs.Add(cnpj.Format(input.WithPunctuation));
        }

        return new GenerateCnpjOutput(cnpjs);
    }
}
