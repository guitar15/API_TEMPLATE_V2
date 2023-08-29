using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtTokenAuthentication;

public static class JwtExtensions
{
    public const string SecurityKey = "sAMQ_6C8@2xaPYj-Z0mx9gcAYu'HN(wYNofJQELo0)X1HEoz)nc/;CSA*FZ_C'B";
    public const int RefreshTokenTTL = 2;
    public static void addJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey)),
                 ValidateIssuer = true,
                 ValidIssuer = "https://localhost:5001",
                 ValidateAudience = false,
                 ValidateIssuerSigningKey = true,
                 
             };
         });

    }

}
