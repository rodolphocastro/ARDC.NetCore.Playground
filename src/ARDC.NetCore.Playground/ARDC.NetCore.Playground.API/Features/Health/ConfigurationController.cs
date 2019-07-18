using ARDC.NetCore.Playground.API.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace ARDC.NetCore.Playground.API.Features.Health
{
    [Route("configs")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IOptions<GitHubSettings> gitHubSettings;

        public ConfigurationController(IOptions<GitHubSettings> gitHubSettings)
        {
            this.gitHubSettings = gitHubSettings ?? throw new ArgumentNullException(nameof(gitHubSettings));
        }

        [HttpGet("github/clientId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetClientId() => Ok(gitHubSettings.Value?.ClientId);

        [HttpGet("github/callback")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCallback() => Ok(gitHubSettings.Value?.CallbackPath);
    }
}
