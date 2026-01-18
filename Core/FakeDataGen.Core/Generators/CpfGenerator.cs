using FakeDataGen.Core.Ports;
using FakeDataGen.Core.ValueObjects;

namespace FakeDataGen.Core.Generators;

public sealed class CpfGenerator : ICpfGenerator
{
    private readonly Random _random = new();

    // ICpfGenerator implementation: returns a Cpf value object
    public Cpf Generate()
    {
        var numbers = GenerateNumbers();
        return Cpf.Create(numbers);
    }

    // Overload kept for convenience: string with/without punctuation
    public string Generate(bool withPunctuation)
    {
        return Generate().Format(withPunctuation);
    }

    private string GenerateNumbers()
    {
        var digits = new int[11];

        for (int i = 0; i < 9; i++)
            digits[i] = _random.Next(0, 10);

        digits[9] = CalculateDigit(digits, 10);
        digits[10] = CalculateDigit(digits, 11);

        return string.Concat(digits);
    }

    private static int CalculateDigit(int[] digits, int factor)
    {
        int sum = 0;

        for (int i = 0; i < factor - 1; i++)
            sum += digits[i] * (factor - i);

        int digit = (sum * 10) % 11;
        return digit == 10 ? 0 : digit;
    }
}