using FakeDataGen.Core.ValueObjects;

namespace FakeDataGen.Core.Ports;

public interface ICnpjGenerator
{
    Cnpj Generate(bool alphanumeric);
}
