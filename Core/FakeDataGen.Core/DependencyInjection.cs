using FakeDataGen.Core.Generators;
using FakeDataGen.Core.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace FakeDataGen.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<ICpfGenerator, CpfGenerator>();
        services.AddSingleton<ICnpjGenerator, CnpjGenerator>();

        return services;
    }
}
