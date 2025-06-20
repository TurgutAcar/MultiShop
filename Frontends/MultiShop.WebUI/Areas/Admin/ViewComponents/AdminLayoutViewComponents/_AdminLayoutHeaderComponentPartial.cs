using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticService;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial:ViewComponent
    {
        private readonly IMessageStatisticService _messageStatisticService;
        private readonly IUserService _userService;
        private readonly ICommentStatisticService _commentStatisticService;

        public _AdminLayoutHeaderComponentPartial(IMessageStatisticService messageStatisticService, IUserService userService, ICommentStatisticService commentStatisticService)
        {
            _messageStatisticService = messageStatisticService;
            _userService = userService;
            _commentStatisticService = commentStatisticService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user=await _userService.GetUserInfo();
            int messageCount = await _messageStatisticService.GetTotalMessageCountByReceiverId(user.Id);
            int totalCommentCount = await _commentStatisticService.GetTotalCommentCount();
            ViewBag.messageCount = messageCount;
            ViewBag.totalCommentCount = totalCommentCount;
            return View();
        }
    }
}
