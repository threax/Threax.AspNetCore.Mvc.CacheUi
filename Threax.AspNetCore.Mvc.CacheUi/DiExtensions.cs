using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Threax.AspNetCore.Mvc.CacheUi;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DiExtensions
    {
        public static IServiceCollection AddThreaxCacheUi(this IMvcBuilder builder, String cacheToken, Action<CacheUiConfig> configureOptions = null)
        {
            var options = new CacheUiConfig();
            configureOptions?.Invoke(options);

            CacheUiUrlHelperExtensions.CacheToken = cacheToken;

            var services = builder.Services;
            services.TryAddSingleton<CacheUiConfig>(options);
            services.TryAddScoped<ICacheUiBuilder>(s =>
            {
                ICacheUiBuilder builder = new CacheUiBuilder(s.GetRequiredService<CacheUiConfig>(), s.GetRequiredService<ICompositeViewEngine>());
                if(options.CustomizeCacheBuilder != null)
                {
                    builder = options.CustomizeCacheBuilder?.Invoke(s, builder);
                }
                return builder;
            });

            return services;
        }
    }
}
