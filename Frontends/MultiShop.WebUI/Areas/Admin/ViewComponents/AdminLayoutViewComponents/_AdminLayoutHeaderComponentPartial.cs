using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Mapping;
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
            var totalMessageResult = await _messageStatisticService.GetTotalMessageCountByReceiverId(user.Data.Id);
            if(!totalMessageResult.IsSuccessful)
            {
             //   ViewBag.InfoMessage = UiMessageMapper.Map(totalMessageResult.Source);

                return View();
            }
            ViewBag.messageCount = totalMessageResult.Data;
           
            var  totalCommentCountResponse = await _commentStatisticService.GetTotalCommentCount();
            if (!totalCommentCountResponse.IsSuccessful)
            {
              //  ViewBag.InfoMessage = UiMessageMapper.Map(totalCommentCountResponse.Source);

                return View();
            }
            ViewBag.totalCommentCount = totalCommentCountResponse.Data;
            return View();
        }
    }
}
