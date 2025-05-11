using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponet
{
    public class _TopbarUILayoutComonentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
        
    }
}
