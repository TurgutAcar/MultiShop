namespace MultiShop.WebUI.Services.NotifierServices
{
    public interface IUiNotifierService
    {
        void Warning(IEnumerable<string> messages);
    }

}
