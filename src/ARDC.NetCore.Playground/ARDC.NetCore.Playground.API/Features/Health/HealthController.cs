using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.API.Features.Health
{
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));
        }

        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {            
            var report = await _healthCheckService.CheckHealthAsync();
            return report.Status == HealthStatus.Healthy ? Ok(report) : StatusCode(StatusCodes.Status503ServiceUnavailable, report);
        }
    }
}
