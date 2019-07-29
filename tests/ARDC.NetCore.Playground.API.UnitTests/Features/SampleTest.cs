using ARDC.NetCore.Playground.API.Features.Samples;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    /// <summary>
    /// Tests for the Sample controller.
    /// </summary>
    public class SampleTest
    {
        private readonly SampleController _controller;

        public SampleTest()
        {
            _controller = new SampleController();
        }

        /// <summary>
        /// It should be possible to get a hello message.
        /// </summary>
        [Fact(DisplayName = "GET Hello")]
        public void GetHello()
        {
            var result = _controller.HelloWorld();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK result");

            var objectResult = result as OkObjectResult;
            objectResult.Value.Should()
                .NotBeNull().And
                .BeOfType<string>();

            objectResult.Value.As<string>().Should()
                .NotBeNullOrWhiteSpace("it should return a string with content").And
                .Contain("Hello World", "it should say HelloWorld!");

        }

        /// <summary>
        /// It should be possible to get the current time.
        /// </summary>
        [Fact(DisplayName = "GET Now")]
        public void GetNow()
        {
            var result = _controller.Now();

            result.Should()
                .NotBeNull().And
                .BeAssignableTo<IActionResult>().And
                .BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Value.Should()
                .NotBeNull().And
                .BeOfType<DateTime>();

            objectResult.Value.As<DateTime>().Should()
                .BeCloseTo(DateTime.Now, 1000, "the Now returned by the Controller could be a few ms in the past");
        }

        /// <summary>
        /// It should be possible to ping-pong the server.
        /// </summary>
        [Fact(DisplayName = "POST Ping")]
        public void PingPong()
        {
            var result = _controller.Pong();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK result").Which
                .Value.Should()
                    .NotBeNull("an object is always expected").And
                    .BeOfType<string>("it should be a simple string").And
                    .Be("Pong!");
        }
    }
}
