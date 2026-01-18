namespace FakeDataGen.Core.ValueObjects;

using System.Text.RegularExpressions;

public sealed class Cnpj
{
    public string Value { get; }

    private const int TamanhoCnpjSemDv = 12;
    private const string RegexCaracteresFormatacao = "[./-]";
    private const string RegexFormacaoBaseCnpj = "[A-Z\\d]{12}";
    private const string RegexFormacaoDv = "[\\d]{2}";
    private const string RegexValorZerado = "^[0]+$";
    private const int ValorBase = (int)'0';

    private static readonly int[] PesosDv =
        [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    private Cnpj(string value)
    {
        Value = value;
    }

    public static Cnpj Create(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("CNPJ inválido");

        return new Cnpj(RemoveCaracteresFormatacao(value));
    }

    public static bool IsValid(string cnpj)
    {
        if (cnpj is null)
            return false;

        cnpj = RemoveCaracteresFormatacao(cnpj);

        if (!IsCnpjFormacaoValidaComDv(cnpj))
            return false;

        var dvInformado = cnpj[TamanhoCnpjSemDv..];
        var dvCalculado = CalculaDv(cnpj[..TamanhoCnpjSemDv]);

        return dvCalculado == dvInformado;
    }

    public static string CalculaDv(string baseCnpj)
    {
        if (baseCnpj is null)
            throw new ArgumentException("CNPJ inválido para cálculo do DV");

        baseCnpj = RemoveCaracteresFormatacao(baseCnpj);

        if (!IsCnpjFormacaoValidaSemDv(baseCnpj))
            throw new ArgumentException(
                $"CNPJ {baseCnpj} não é válido para o cálculo do DV");

        var dv1 = CalculaDigito(baseCnpj).ToString();
        var dv2 = CalculaDigito(baseCnpj + dv1).ToString();

        return dv1 + dv2;
    }

    private static int CalculaDigito(string cnpj)
    {
        int soma = 0;

        for (int indice = cnpj.Length - 1; indice >= 0; indice--)
        {
            int valorCaracter = cnpj[indice] - ValorBase;

            soma += valorCaracter *
                    PesosDv[PesosDv.Length - cnpj.Length + indice];
        }

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }

    public string Format(bool withPunctuation)
    {
        return withPunctuation
            ? $"{Value[..2]}.{Value[2..5]}.{Value[5..8]}/{Value[8..12]}-{Value[12..]}"
            : Value;
    }

    private static string RemoveCaracteresFormatacao(string cnpj)
        => Regex.Replace(cnpj.Trim(), RegexCaracteresFormatacao, "");

    private static bool IsCnpjFormacaoValidaSemDv(string cnpj)
        => Regex.IsMatch(cnpj, RegexFormacaoBaseCnpj) &&
           !Regex.IsMatch(cnpj, RegexValorZerado);

    private static bool IsCnpjFormacaoValidaComDv(string cnpj)
        => Regex.IsMatch(cnpj, RegexFormacaoBaseCnpj + RegexFormacaoDv) &&
           !Regex.IsMatch(cnpj, RegexValorZerado);
}

