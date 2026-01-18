using FakeDataGen.Core.ValueObjects;

namespace FakeDataGen.Core.Tests;

public class CpfTests
{
    [Fact]
    public void IsValid_ReturnsTrue_ForKnownValidCpf()
    {
        var valid = Cpf.IsValid("529.982.247-25"); // exemplo conhecido
        Assert.True(valid);
    }

    [Theory]
    [InlineData("111.111.111-11")]
    [InlineData("123")]
    [InlineData("52998224726")] // dígito verificador errado
    public void IsValid_ReturnsFalse_ForInvalidCpfs(string cpf)
    {
        Assert.False(Cpf.IsValid(cpf));
    }

    [Fact]
    public void Create_Throws_ForInvalidCpf()
    {
        Assert.Throws<ArgumentException>(() => Cpf.Create("123"));
    }

    [Fact]
    public void Format_ReturnsCorrectPunctuation()
    {
        var obj = Cpf.Create("52998224725");
        Assert.Equal("529.982.247-25", obj.Format(true));
        Assert.Equal("52998224725", obj.Format(false));
    }
}
