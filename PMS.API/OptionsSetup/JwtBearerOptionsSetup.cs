using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.DTOs.Options;
using PMS.Infrastructure.Authentication;

namespace PMS.API.OptionsSetup
{
    //public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    //{
    //    private readonly JwtOptions _jwtOptions;

    //    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    //    {
    //        _jwtOptions = jwtOptions.Value;
    //    }

    //    public void PostConfigure(string? name, JwtBearerOptions options)
    //    {
    //        options.TokenValidationParameters.ValidIssuer = _jwtOptions.Issuer;
    //        options.TokenValidationParameters.ValidAudience = _jwtOptions.Audience;

    //        options.TokenValidationParameters.IssuerSigningKey =
    //            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

    //        //options.TokenValidationParameters.IssuerSigningKey =
    //        //    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

    //        //options.TokenValidationParameters.IssuerSigningKey = 
    //        //    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SecretKey));

    //    }
    //}

    public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {

                ValidIssuer = "https://localhost:7155",
                ValidAudience = "https://localhost:7155",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("87c0bfad20d0525ec8bcd2f136cee305f33c14bf007400bd5f4f0e840843680c")),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true

                //    ValidIssuer = _jwtOptions.Issuer,
                //    ValidAudience = _jwtOptions.Audience,
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                //    ValidateIssuer = true,
                //    ValidateAudience = true,
                //    ValidateLifetime = true,
                //    ValidateIssuerSigningKey = true


                //ValidateIssuer = true,
                //ValidateAudience = true,
                //ValidateLifetime = true,
                ////ValidateIssuerSigningKey = false,
                ////SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                ////{
                ////    var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token);

                ////    return jwt;
                ////},
                //ValidIssuer = _jwtOptions.Issuer,
                //ValidAudience = _jwtOptions.Audience,



                //IssuerSigningKey = new SymmetricSecurityKey(
                //    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };
        }
    }
}
