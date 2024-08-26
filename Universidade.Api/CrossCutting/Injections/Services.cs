//using Autenticacao.Api.CrossCutting.Configuration;
//using Autenticacao.Api.Domain.UserContext.Handler;
//using Autenticacao.Api.Infra.UserContext.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
namespace Universidade.Api.CrossCutting.Injections;
public static class Services
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var secret = configuration.GetSection(SecretSettings.Key).Get<SecretSettings>();
        //var key = Encoding.ASCII.GetBytes(secret.ToString());

        //services.AddTransient<AuthenticationHandler>();
        //services.AddTransient<TokenService>();
        //services
        //    .AddAuthentication(x =>
        //    {
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(x =>
        //    {
        //        x.RequireHttpsMetadata = false;
        //        x.SaveToken = true;
        //        x.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };
        //    });

        return services;
    }
}
