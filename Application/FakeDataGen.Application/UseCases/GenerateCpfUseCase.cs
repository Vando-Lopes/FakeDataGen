using FakeDataGen.Application.Exceptions;
using FakeDataGen.Application.UseCases.Records.Cpf;
using FakeDataGen.Core.Ports;

namespace FakeDataGen.Application.UseCases;

public sealed class GenerateCpfUseCase(ICpfGenerator generator)
{
    public GenerateCpfOutput Execute(GenerateCpfInput input)
    {
        if (input.Quantity <= 0 || input.Quantity > 100)
            throw new ValidationException("A quantidade deve estar entre 1 e 100");

        var cpfs = new List<string>();

        for (int i = 0; i < input.Quantity; i++)
        {
            var cpf = generator.Generate();
            cpfs.Add(cpf.Format(input.WithPunctuation));
        }

        return new GenerateCpfOutput(cpfs);
    }
}
