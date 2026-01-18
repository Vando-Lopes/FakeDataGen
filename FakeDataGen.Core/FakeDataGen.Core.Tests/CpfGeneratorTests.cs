using FakeDataGen.Core.Generators;
using FakeDataGen.Core.ValueObjects;

namespace FakeDataGen.Core.Tests;

public class CpfGeneratorTests
{
    [Fact]
    public void Generate_ReturnsValidCpf_WithoutPunctuation()
    {
        var gen = new CpfGenerator();
        var cpf = gen.Generate(false);

        Assert.Equal(11, cpf.Length);
        Assert.True(Cpf.IsValid(cpf));
    }

    [Fact]
    public void Generate_ReturnsValidCpf_WithPunctuation()
    {
        var gen = new CpfGenerator();
        var cpf = gen.Generate(true);

        Assert.Contains(".", cpf);
        Assert.Contains("-", cpf);
        Assert.True(Cpf.IsValid(cpf));
    }
}
