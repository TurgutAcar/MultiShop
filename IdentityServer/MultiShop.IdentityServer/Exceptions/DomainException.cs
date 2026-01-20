using MultiShop.Shared.Enums;

namespace MultiShop.IdentityServer.Exceptions
{
    public abstract class DomainException : System.Exception
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
