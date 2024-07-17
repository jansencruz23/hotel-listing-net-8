using Asp.Versioning;
using AspNetCoreRateLimit;
using Marvin.Cache.Headers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        { 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly()));

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (expirationOptions) =>
                {
                    expirationOptions.MaxAge = 120;
                    expirationOptions.CacheLocation = CacheLocation.Private;
                },
                (validationOptions) =>
                {
                    validationOptions.MustRevalidate = true;
                }
            );

            services.AddMemoryCache();
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 1, // how many calls
                    Period = "5s" // per 5s
                }
            };

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            return services;
        }
    }
}
