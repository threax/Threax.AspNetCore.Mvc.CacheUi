using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public class CacheUiResult
    {
        public IActionResult ActionResult { get; set; }

        public bool UsingCacheRoot { get; set; }
    }
}
