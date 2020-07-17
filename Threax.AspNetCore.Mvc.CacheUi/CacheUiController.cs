using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public class CacheUiController : Controller
    {
        public CacheUiController(ICacheUiBuilder builder)
        {
            CacheUiBuilder = builder;
        }

        public ICacheUiBuilder CacheUiBuilder { get; }

        public async Task<IActionResult> CacheUiView(string view = null, object model = null, string cacheToken = null)
        {
            var result = await this.CacheUiBuilder.Build(this, cacheToken: cacheToken, view: view, model: model);
            return result.ActionResult;
        }
    }
}
