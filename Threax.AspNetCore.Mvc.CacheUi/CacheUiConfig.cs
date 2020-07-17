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
        /// A format string to use when writing the title to the page. This is javascript, so it can be changed as needed. 
        /// The template quotes should be used, since the string will be escaped for them.
        /// Defaults to 'document.title=`{0} ${{document.title}}`;' to write the page's title, a space, then the existing title.
        /// </summary>
        public String TitleFormat { get; set; } = "document.title=`{0} ${{document.title}}`;";

        /// <summary>
        /// The name of the cache root page. This is rendered in place of the content we want to cache on the client side. Default '_CacheRoot'
        /// </summary>
        public object CacheRootView { get; set; } = "_CacheRoot";

        /// <summary>
        /// The name of the layout to use when rendering an embedded page. This is the root page used when sending the rendered view javascript to get cached. Default: '_CachePage'
        /// </summary>
        public string CachePageLayout { get; set; } = "_CachePage";
    }
}
