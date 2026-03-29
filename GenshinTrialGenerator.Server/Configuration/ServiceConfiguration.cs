using GenshinTrialGenerator.Application.Services.BossServices;
using GenshinTrialGenerator.Application.Services.HeroServices;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Infrastructure.Data;
using GenshinTrialGenerator.Infrastructure.Repositories;
using System.Runtime.CompilerServices;

namespace GenshinTrialGenerator.Server.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(
                    typeof(BossRepository).Assembly,
                    typeof(CreateHeroService).Assembly
                )
                .AddClasses(classes => classes.InNamespaces(
                    "GenshinTrialGenerator.Application.Services",
                    "GenshinTrialGenerator.Infrastructure.Repositories"
                ))
                .AsMatchingInterface()
                .WithScopedLifetime()
            );

            services.AddSingleton<IStorageService, StorageService>();

            return services;
        }
    }
}
