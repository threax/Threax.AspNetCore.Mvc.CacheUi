using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    //Move this to a library
    public static class CacheUiUrlHelperExtensions
    {
        private const string ControllerSuffix = "Controller";

        internal static String CacheToken { get; set; }

        public static string CacheUiActionLink(this IUrlHelper helper, string action = null, Type controller = null, string fragment = null)
        {
            var values = new { cacheToken = CacheToken };
            string controllerName = GetControllerName(controller);
            return helper.ActionLink(action, controllerName, values, "https", helper.ActionContext.HttpContext.Request.Host.Value, fragment);
        }

        public static string ActionLink(this IUrlHelper helper, string action = null, Type controller = null, string fragment = null)
        {
            string controllerName = GetControllerName(controller);
            return helper.ActionLink(action, controllerName, null, "https", helper.ActionContext.HttpContext.Request.Host.Value, fragment);
        }

        private static string GetControllerName(Type controller)
        {
            var controllerName = controller.Name;
            if (controllerName.EndsWith(ControllerSuffix))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - ControllerSuffix.Length);
            }

            return controllerName;
        }
    }
}
