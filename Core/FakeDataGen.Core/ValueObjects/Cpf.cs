namespace FakeDataGen.Core.ValueObjects;

public sealed class Cpf
{
    public string Value { get; }

    private Cpf(string value)
    {
        Value = value;
    }

    public static Cpf Create(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("CPF inválido");

        return new Cpf(value);
    }

    public static bool IsValid(string cpf)
    {
        cpf = OnlyNumbers(cpf);

        if (cpf.Length != 11)
            return false;

        if (cpf.All(c => c == cpf[0]))
            return false;

        int[] digits = [.. cpf.Select(c => c - '0')];

        int sum1 = 0;
        for (int i = 0; i < 9; i++)
            sum1 += digits[i] * (10 - i);

        int d1 = (sum1 * 10) % 11;
        if (d1 == 10) d1 = 0;

        if (digits[9] != d1)
            return false;

        int sum2 = 0;
        for (int i = 0; i < 10; i++)
            sum2 += digits[i] * (11 - i);

        int d2 = (sum2 * 10) % 11;
        if (d2 == 10) d2 = 0;

        return digits[10] == d2;
    }

    public string Format(bool withPunctuation)
    {
        var numbers = OnlyNumbers(Value);

        return withPunctuation
            ? $"{numbers[..3]}.{numbers[3..6]}.{numbers[6..9]}-{numbers[9..]}"
            : numbers;
    }

    private static string OnlyNumbers(string input)
        => new([.. input.Where(char.IsDigit)]);
}