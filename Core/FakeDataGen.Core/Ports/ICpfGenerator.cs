using FakeDataGen.Core.ValueObjects;

namespace FakeDataGen.Core.Ports;

public interface ICpfGenerator
{
    Cpf Generate();
}
