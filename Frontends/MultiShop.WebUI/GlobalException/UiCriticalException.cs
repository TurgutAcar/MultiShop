using MultiShop.Shared.Enums;

namespace MultiShop.WebUI.GlobalException
{
    public class UiCriticalException : Exception
    {
        public UiCriticality Criticality { get; }

        public UiCriticalException(string message)
            : this(UiCriticality.High, new[] { message })
        {
        }

        public UiCriticalException(IEnumerable<string> messages)
            : this(UiCriticality.High, messages)
        {
        }

        public UiCriticalException(
            UiCriticality criticality,
            IEnumerable<string> messages)
            : base(string.Join(" | ", messages))
        {
            Criticality = criticality;
        }
    }


}
