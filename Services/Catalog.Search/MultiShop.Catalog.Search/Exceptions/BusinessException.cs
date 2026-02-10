using MultiShop.Shared.Enums;

namespace MultiShop.Catalog.Search.Exceptions
{
    public sealed class BusinessException : DomainException
    {
        public List<string> Errors { get; }

        public BusinessException(
            string message,
            int statusCode = StatusCodes.Status400BadRequest,
            UiCriticality criticality = UiCriticality.Medium)
            : base(message, statusCode, criticality)
        {
            Errors = new List<string> { message };
        }

        public BusinessException(
            List<string> errors,
            int statusCode = StatusCodes.Status400BadRequest,
            UiCriticality criticality = UiCriticality.Medium)
            : base("İş kuralı ihlali.", statusCode, criticality)
        {
            Errors = errors;
        }
    }

}
