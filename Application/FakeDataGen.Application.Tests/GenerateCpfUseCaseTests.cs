using FakeDataGen.Application.Exceptions;
using FakeDataGen.Application.UseCases;
using FakeDataGen.Core.Ports;
using FakeDataGen.Core.ValueObjects;
using Moq;

namespace FakeDataGen.Application.Tests;

public class GenerateCpfUseCaseTests
{
    [Fact]
    public void Execute_ThrowsValidationException_WhenQuantityOutOfRange()
    {
        var mockGen = new Mock<ICpfGenerator>();
        var useCase = new GenerateCpfUseCase(mockGen.Object);

        var input = new GenerateCpfInput(0, true);

        Assert.Throws<ValidationException>(() => useCase.Execute(input));
    }

    [Fact]
    public void Execute_ReturnsExpectedNumberOfCpfs_AndCallsGeneratorNtimes()
    {
        var mockGen = new Mock<ICpfGenerator>();
        mockGen.Setup(g => g.Generate()).Returns(Cpf.Create("52998224725"));

        var useCase = new GenerateCpfUseCase(mockGen.Object);

        var output = useCase.Execute(new GenerateCpfInput(3, true));

        Assert.Equal(3, output.Cpfs.Count);
        mockGen.Verify(g => g.Generate(), Times.Exactly(3));
    }
}
