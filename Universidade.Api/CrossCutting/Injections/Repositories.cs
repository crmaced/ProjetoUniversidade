//using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using Universidade.Api.CrossCutting.Configuration;
using Universidade.Api.Domain.Interfaces.Repository;
using Universidade.Api.Domain.Repository;
using Universidade.Api.Infra.DataBase;

namespace Universidade.Api.CrossCutting.Injections;

public static class Repositories
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConect = configuration.GetSection(DataBaseConfiguration.Key).Get<DataBaseConfiguration>();

        services.AddTransient<IDbConnection>(x => new SqlConnection(sqlConect.ToString()));

        services.AddTransient<IRepository>(x =>
        {
            return new Repository( new DbContext(sqlConect.ToString()));
        });

        return services;
    }
}
