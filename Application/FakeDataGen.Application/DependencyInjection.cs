using FakeDataGen.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FakeDataGen.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GenerateCpfUseCase>();
        services.AddScoped<GenerateCnpjUseCase>();
        return services;
    }
}
