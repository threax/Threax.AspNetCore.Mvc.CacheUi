using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public class CacheUiRenderData : ICacheUiRenderData
    {
        public String Title { get; set; }

        public string Layout { get; set; }

        public string ContentLink { get; set; }
    }
}
