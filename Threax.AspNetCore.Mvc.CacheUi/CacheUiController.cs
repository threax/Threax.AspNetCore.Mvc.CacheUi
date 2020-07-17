using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    /// <summary>
    /// You can inherit from this controller to gain the CacheUiView helper method.
    /// This is optional. You can also use ICacheUiBuilder directly.
    /// </summary>
    public class CacheUiController : Controller
    {
        public CacheUiController(ICacheUiBuilder builder)
        {
            CacheUiBuilder = builder;
        }

        public ICacheUiBuilder CacheUiBuilder { get; }

        public async Task<IActionResult> CacheUiView(string view = null, object model = null, string cacheToken = null)
        {
            return await this.CacheUiBuilder.Build(this, view, model, cacheToken);
        }
    }
}
