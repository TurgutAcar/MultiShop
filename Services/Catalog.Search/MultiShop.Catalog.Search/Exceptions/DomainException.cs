using MultiShop.Shared.Enums;

namespace MultiShop.Catalog.Search.Exceptions
{
    public abstract class DomainException : Exception
    {
        public int StatusCode { get; }
        public UiCriticality Criticality { get; }

        protected DomainException(
            string message,
            int statusCode,
            UiCriticality criticality) : base(message)
        {
            StatusCode = statusCode;
            Criticality = criticality;
        }
    }

}
