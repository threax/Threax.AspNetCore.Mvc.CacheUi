using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Mvc.CacheUi
{
    public interface ICacheUiBuilder
    {
        Task<CacheUiResult> Build(Controller controller, string view = null, object model = null, string cacheToken = null);
    }
}