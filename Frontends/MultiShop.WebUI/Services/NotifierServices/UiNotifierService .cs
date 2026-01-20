using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Services.NotifierServices
{
    public class UiNotifierService : IUiNotifierService
    {
        private readonly UiHealthState _uiHealthState;
        public UiNotifierService( UiHealthState uiHealthState)
        {
            _uiHealthState = uiHealthState;
        }

        public void Warning(IEnumerable<string> messages)
        {
            _uiHealthState.HasWarnings = true;
            _uiHealthState.Warnings.AddRange(messages);
        }
    }

}
