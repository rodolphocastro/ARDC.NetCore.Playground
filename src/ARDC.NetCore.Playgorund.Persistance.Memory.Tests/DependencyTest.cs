using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Memory.Repositories;
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

        /// <summary>
        /// It should be possible to resolve the Game Repository.
        /// </summary>
        [Fact(DisplayName = "Resolve Game Repository")]
        public void ResolveGameRepository()
        {
            var unitOfWork = Provider.GetService<IUnitOfWork>();
            var gameRepo = unitOfWork.GameRepository;

            gameRepo.Should()
                .NotBeNull("it should be created by the Unit Of Work").And
                .BeAssignableTo<IGameRepository>("it should be an implementation of IGameRepository").And
                .BeOfType<GameRepository>("it should be an InMemory Game Repository");
        }

        /// <summary>
        /// It should be possible to resolve the Review Repository.
        /// </summary>
        [Fact(DisplayName = "Resolve Review Repository")]
        public void ResolveReviewRepository()
        {
            var unitOfWork = Provider.GetService<IUnitOfWork>();
            var reviewRepo = unitOfWork.ReviewRepository;

            reviewRepo.Should()
                .NotBeNull("it should be created by the Unit Of Work").And
                .BeAssignableTo<IReviewRepository>("it should be an implementation of IReviewRepository").And
                .BeOfType<ReviewRepository>("it should be an InMemory Review Repository");
        }
    }
}
