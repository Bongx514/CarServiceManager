using Microsoft.AspNetCore.Mvc;

namespace healthcare.ViewComponents
{
    public class PageLoaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
