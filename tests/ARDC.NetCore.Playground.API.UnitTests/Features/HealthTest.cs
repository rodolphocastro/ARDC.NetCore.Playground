using ARDC.NetCore.Playground.API.Features.Health;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    public class HealthTest
    {
        private readonly HealthController _controller;

        public HealthTest()
        {
            _controller = new HealthController(new FakeHealthCheckService());
        }

        /// <summary>
        /// It should be possible to get the current health status of the API.
        /// </summary>
        [Fact(DisplayName = "GET Status")]
        public async Task GetHealthStatus()
        {
            var result = await _controller.GetHealth();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult");
        }

        /// <summary>
        /// This class implements a fake HealthCheckService for testing purposes.
        /// </summary>
        internal class FakeHealthCheckService : HealthCheckService
        {
            public override Task<HealthReport> CheckHealthAsync(Func<HealthCheckRegistration, bool> predicate, CancellationToken cancellationToken = default)
            {
                var entries = new Dictionary<string, HealthReportEntry>();
                entries.Add("pudim", new HealthReportEntry(HealthStatus.Healthy, "", TimeSpan.FromMilliseconds(1), null, null));

                return Task.FromResult(new HealthReport(entries, TimeSpan.FromMilliseconds(1)));
            }

        }
    }


}
