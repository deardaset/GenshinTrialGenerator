using GenshinTrialGenerator.Application.Mappings;
using GenshinTrialGenerator.Infrastructure.Mappings;

namespace GenshinTrialGenerator.API.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<BossProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<HeroProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<BossEntityProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<HeroEntityProfile>());

            //---THIS IS BETTER THAN UPPER VARIANT BUT DOESN'T WORK BECAUSE OF VERSIONS---
            //services.AddAutoMapper(
            //    typeof(HeroProfile).Assembly,
            //    typeof(HeroEntityProfile).Assembly
            //);

            return services;
        }
    }
}
