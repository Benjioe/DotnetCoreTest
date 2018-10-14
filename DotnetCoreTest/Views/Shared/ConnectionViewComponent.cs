using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreTest.Views.Shared
{
    public class ConnectionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string monParam)
        {
            //Essaye de rendre
            //Views/<controller >/Components/<view_component >/<view_name>
            //Puis
            //Views/Shared/Components/<view_component >/<view_name>
            return View(monParam);
        }
    }
}