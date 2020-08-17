using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public class CacheUiBuilder : ICacheUiBuilder
    {
        private static readonly ConcurrentDictionary<String, String> viewCache = new ConcurrentDictionary<string, string>();

        private readonly CacheUiConfig config;
        private readonly ICompositeViewEngine viewEngine;
        private readonly ICacheUiRenderData renderData;

        public CacheUiBuilder(CacheUiConfig config, ICompositeViewEngine viewEngine, ICacheUiRenderData renderData)
        {
            this.config = config;
            this.viewEngine = viewEngine;
            this.renderData = renderData;
        }

        public async Task<IActionResult> Build(Controller controller, string view = null, object model = null, string cacheToken = null)
        {
            controller.ViewData.Model = model;

            if (cacheToken == null)
            {
                cacheToken = controller.Request.RouteValues["cacheToken"]?.ToString();
            }

            var action = controller.Request.RouteValues["action"]?.ToString();
            if (action == null)
            {
                throw new InvalidOperationException("Cannot determine action for handling cache. Is the Request.ReouteValue 'action' set?");
            }

            if (view == null)
            {
                view = action;
            }

            controller.Request.RouteValues.Remove("cacheToken");
            controller.RouteData.Values.Remove("cacheToken");
            if (cacheToken != null) //If there is a token handle the view like normal
            {
                //Get cached view
                var viewKey = $"{controller.GetType().FullName}|{action}|{view}";

                String viewString;
                //Always render if not in the cache
                if (!viewCache.TryGetValue(viewKey, out viewString))
                {
                    //Render and escape view
                    renderData.Layout = config.CachePageLayout;
                    viewString = await this.RenderView(controller, view);
                    viewString = $"document.write(`{EscapeTemplateString(viewString)}`);";
                    if(renderData.Title != null)
                    {
                        viewString += String.Format(config.TitleFormat, EscapeTemplateString(renderData.Title));
                    }

                    if (cacheToken != config.NoCacheModeToken) //Only cache the view when not in nocache mode. Otherwise take as is.
                    {
                        viewString = viewCache.GetOrAdd(viewKey, viewString);
                    }
                }

                //Handle cache mode
                if (cacheToken != config.NoCacheModeToken)
                {
                    controller.HttpContext.Response.Headers["Cache-Control"] = config.CacheControlHeader;
                }
                else
                {
                    controller.HttpContext.Response.Headers["Cache-Control"] = "no-store"; //Force no cache if requested.
                }

                //Create result
                return controller.Content(viewString, "application/javascript");
            }
            else
            {
                //Otherwise send root view, which will use the cache or request the rest of the page from the server
                //Handle cache mode
                controller.HttpContext.Response.Headers["Cache-Control"] = "no-store"; //No caching for the entry page.

                //Create result
                renderData.ContentLink = controller.Url.CacheUiActionLink(action, controller.GetType());
                return controller.View(viewName: config.CacheRootView);
            }
        }

        private async Task<string> RenderView(Controller controller, string viewName)
        {
            //Adapted from Red at https://stackoverflow.com/questions/40912375/return-view-as-string-in-net-core
            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, true);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        public static String EscapeTemplateString(string input)
        {
            //Adapted from https://benohead.com/blog/2014/01/14/how-to-create-a-properly-escaped-javascript-string-in-razor/
            //This version escapes template strings, so \r and \n are allowed and ` and ${ are handled
            StringBuilder builder = new StringBuilder();
            // Then add each character properly escaping them
            char previous = '\0';
            foreach (char c in input)
            {
                switch (c)
                {
                    //First check whether it's one of the defined escape sequences
                    case '\'': //single quote
                        builder.Append("\\\'");
                        break;
                    case '\"': //double quote
                        builder.Append("\\\"");
                        break;
                    case '\\': //backslash
                        builder.Append("\\\\");
                        break;
                    case '\0': //Unicode character 0
                        builder.Append("\\0");
                        break;
                    case '\a': //Alert (character 7)
                        builder.Append("\\a");
                        break;
                    case '\b': //Backspace (character 8)
                        builder.Append("\\b");
                        break;
                    case '\f': //Form feed (character 12)
                        builder.Append("\\f");
                        break;
                    case '\t': //Horizontal tab (character 9)
                        builder.Append("\\t");
                        break;
                    case '\v': //Vertical quote (character 11)
                        builder.Append("\\v");
                        break;
                    case '`':
                        builder.Append("\\`");
                        break;
                    case '{':
                        if (previous == '$')
                        {
                            builder.Remove(builder.Length - 1, 1);
                            builder.Append("\\${");
                        }
                        else
                        {
                            builder.Append(c);
                        }
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
                previous = c;
            }
            return builder.ToString();
        }
    }
}
