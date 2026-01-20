namespace MultiShop.WebUI.Models
{
    public class UiHealthState
    {
        public bool HasWarnings { get; set; }
        public List<string> Warnings { get; set; } = new();
    }

}
