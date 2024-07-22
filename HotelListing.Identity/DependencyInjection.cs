using HotelListing.Application.Abstractions.Identity;
using HotelListing.Application.Contracts.Identity;
using HotelListing.Application.Models.Identity;
using HotelListing.Identity.Models;
using HotelListing.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsConfig = configuration.GetSection("JwtSettings");
            var jwtKey = Environment.GetEnvironmentVariable("HOTELLISTING_JWT_KEY");
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<IUserService, UserService>();
            services.Configure<JwtSettings>(jwtSettingsConfig);
            services.AddDbContext<HotelListingIdentityDbContext>(options =>
                options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(HotelListingIdentityDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<HotelListingIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtSettingsConfig.GetSection("Issuer").Value,
                        ValidAudience = jwtSettingsConfig.GetSection("Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            return services;
        }
    }
}
