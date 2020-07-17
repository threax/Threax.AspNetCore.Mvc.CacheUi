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
        /// The name of the cache root page. Default 'CacheRoot'
        /// </summary>
        public object CacheRootView { get; set; } = "CacheRoot";

        /// <summary>
        /// The name of the layout to use when rendering an embedded page.
        /// </summary>
        public string EmbeddedLayout { get; set; } = "_Embedded";
    }
}
