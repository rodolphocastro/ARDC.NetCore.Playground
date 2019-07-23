using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Memory.Tests
{
    public class DependencyTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly ServiceProviderFixture _fixture;
        private ServiceProvider Provider => _fixture.Provider;

        public DependencyTest(ServiceProviderFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        /// <summary>
        /// It should be possible to resolve the PlaygroundContext from the provider.
        /// </summary>
        [Fact(DisplayName = "Resolve Context")]
        public void ResolveDbContext()
        {
            var dbContext = Provider.GetService<PlaygroundContext>();

            dbContext.Should()
                .NotBeNull("it should be created by the provider").And
                .BeAssignableTo<DbContext>("it should inherit DbContext").And
                .BeOfType<PlaygroundContext>("it should be a Playground Context");

            dbContext.Games.Should().NotBeNull("it should be created by EntityFramework");

            dbContext.Reviews.Should().NotBeNull("it should be created by EntityFramework");
        }
    }
}
