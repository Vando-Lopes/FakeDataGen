using FakeDataGen.Core.Ports;
using FakeDataGen.Core.ValueObjects;
using System.Text;

namespace FakeDataGen.Core.Generators;

public sealed class CnpjGenerator : ICnpjGenerator
{
    private static readonly Random Random = new();

    public Cnpj Generate(bool alphanumeric)
    {
        return alphanumeric
            ? GenerateAlphanumeric()
            : GenerateNumeric();
    }

    private static Cnpj GenerateNumeric()
    {
        var baseCnpj = GenerateNumbers(12);
        var dv = Cnpj.CalculaDv(baseCnpj);
        return Cnpj.Create(baseCnpj + dv);
    }

    private static Cnpj GenerateAlphanumeric()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var sb = new StringBuilder(12);

        for (int i = 0; i < 12; i++)
            sb.Append(chars[Random.Next(chars.Length)]);

        var baseCnpj = sb.ToString();
        var dv = Cnpj.CalculaDv(baseCnpj);
        return Cnpj.Create(baseCnpj + dv);
    }

    private static string GenerateNumbers(int length)
    {
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
            sb.Append(Random.Next(10));
        return sb.ToString();
    }
}
