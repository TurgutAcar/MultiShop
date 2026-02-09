using MultiShop.Shared.Dtos;
using MultiShop.Shared.Responses;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.OcelotGateway.DelegateHanders
{
    public class ResultNormalizationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content == null)
                return response;

            var body = await response.Content.ReadAsStringAsync();

            // Validation ProblemDetails normalize
            if (!response.IsSuccessStatusCode && body.Contains("\"errors\""))
            {
                var problem = JsonConvert.DeserializeObject<ValidationProblemDetailsDto>(body);

                var errors = problem.Errors.SelectMany(x => x.Value).ToList();

                var result = Result<object>.Failure((int)response.StatusCode, errors);

                response.Content = new StringContent(
                    JsonConvert.SerializeObject(result),
                    Encoding.UTF8,
                    "application/json"
                );
            }

            return response;
        }
    }

}
