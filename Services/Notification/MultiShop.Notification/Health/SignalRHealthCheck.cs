using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MultiShop.Notification.Hubs;

namespace MultiShop.Notification.Health
{
    public class SignalRHealthCheck : IHealthCheck
    {
        private readonly IServiceProvider _serviceProvider;

        public SignalRHealthCheck(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var hub = scope.ServiceProvider.GetService<CheckoutHub>();

                return Task.FromResult(
                    hub is not null
                        ? HealthCheckResult.Healthy("SignalR hub is available")
                        : HealthCheckResult.Unhealthy("SignalR hub is not available")
                );
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy("SignalR hub failed", ex)
                );
            }
        }
    }

}
