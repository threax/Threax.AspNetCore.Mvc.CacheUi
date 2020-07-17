using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public class CacheUiConfig
    {
        /// <summary>
        /// The value to set for the CacheControlHeader. Default: 'private, max-age=2592000, stale-while-revalidate=86400, immutable'
        /// </summary>
        public String CacheControlHeader { get; set; } = "private, max-age=2592000, stale-while-revalidate=86400, immutable";

        /// <summary>
        /// Set this to the string you want to use for no cache mode for cached content. Content served under this path will never be cached. Default: 'nocache'
        /// </summary>
        public String NoCacheModeToken { get; set; } = "nocache";

        /// <summary>
        /// Implement this funciton to create a customized cache ui builder. You will get the default one as an argument and it can be wrapped.
        /// </summary>
        public Func<IServiceProvider, ICacheUiBuilder, ICacheUiBuilder> CustomizeCacheBuilder { get; set; }
    }
}
