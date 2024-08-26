using Universidade.Api.CrossCutting.Configuration;

namespace Universidade.Api.CrossCutting.Injections;

public static class Settings
{
    public static IServiceCollection InjectConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataBaseConfiguration>(configuration.GetSection(DataBaseConfiguration.Key));
        //services.Configure<SecretSettings>(configuration.GetSection(SecretSettings.Key));
        return services;
    }
}
