namespace Threax.AspNetCore.Mvc.CacheUi
{
    public interface ICacheUiRenderData
    {
        /// <summary>
        /// The title of the page. This is intended to be used to send the title back to the CacheUiBuilder so it can have javascript change the page title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The current layout to use. Access from your start page and use the value here. Can be null so provide another default.
        /// </summary>
        string Layout { get; set; }

        /// <summary>
        /// The content link to render on the page. Can be null.
        /// </summary>
        string ContentLink { get; set; }
    }
}