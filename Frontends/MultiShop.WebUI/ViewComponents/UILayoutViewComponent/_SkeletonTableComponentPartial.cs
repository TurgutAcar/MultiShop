using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponent
{
    public class _SkeletonTableComponentPartial : ViewComponent
    {
      
            public IViewComponentResult Invoke()
            {
                return View();
            }
        
    }
}
