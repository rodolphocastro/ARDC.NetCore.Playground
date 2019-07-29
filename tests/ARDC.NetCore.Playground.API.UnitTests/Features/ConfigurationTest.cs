using ARDC.NetCore.Playground.API.Features.Health;
using ARDC.NetCore.Playground.API.Settings;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    public class ConfigurationTest
    {
        private readonly GitHubSettings _fakeGitHubSettings;
        private readonly IOptions<GitHubSettings> _fakeOptions;
        private readonly ConfigurationController _controller;

        public ConfigurationTest()
        {
            _fakeGitHubSettings = new GitHubSettings
            {
                ClientId = "myClientId",
                CallbackPath = "/not/a/path"
            };

            _fakeOptions = Options.Create(_fakeGitHubSettings);

            _controller = new ConfigurationController(_fakeOptions);
        }

        /// <summary>
        /// It should be possible to retrieve the Current ClientId.
        /// </summary>
        [Fact(DisplayName = "Get GitHub ClientId")]
        public void GetGitHubClientId()
        {
            var result = _controller.GetClientId();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OkObjectResult").Which
                .Value.Should()
                    .NotBeNull("its value should not be null").And
                    .BeOfType<string>("be a string").And
                    .BeEquivalentTo(_fakeGitHubSettings.ClientId, "be the same clientId as the GitHubSettings");
        }

        /// <summary>
        /// It should be possible to retrieve the Current Callback url.
        /// </summary>
        [Fact(DisplayName = "Get GitHub Callback")]
        public void GetGitHubCallbackUrl()
        {
            var result = _controller.GetCallback();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OkObjectResult").Which
                .Value.Should()
                    .NotBeNull("its value should not be null").And
                    .BeOfType<string>("be a string").And
                    .BeEquivalentTo(_fakeGitHubSettings.CallbackPath, "be the same callbackUrl as the GitHubSettings");
        }
    }
}
