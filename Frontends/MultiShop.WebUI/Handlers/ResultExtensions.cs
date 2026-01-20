using MultiShop.Shared.Enums;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Services.NotifierServices;

namespace MultiShop.WebUI.Handlers
{
    public static class ResultExtensions
    {
        public static T HandleUiResult<T>(
            this Result<T> result,
            IUiNotifierService notificationService)
        {
            if (result.IsSuccessful)
                return result.Data;

            switch (result.Criticality)
            {
                case UiCriticality.Low:
                    return default!;

                case UiCriticality.Medium:
                    notificationService.Warning(result.ErrorMessages);
                    return default!;

                case UiCriticality.High:
                    throw new UiCriticalException(
                        result.Criticality,
                        result.ErrorMessages);
            }

            return default!;
        }
    }

}
