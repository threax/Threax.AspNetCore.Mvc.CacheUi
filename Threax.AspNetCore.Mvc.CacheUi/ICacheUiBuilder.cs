using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public interface ICacheUiBuilder
    {
        Task<CacheUiResult> HandleCache(Controller controller, string cacheToken, string view = null, object model = null);
    }
}