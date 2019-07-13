using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using ARDC.NetCore.Playground.Persistance.Mock.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Mock.Tests
{
    /// <summary>
    /// Tests for the Persistence.Mock dependency injection.
    /// </summary>

    public class DependencyTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly ServiceProviderFixture _providerFixture;
        private ServiceProvider Provider => _providerFixture.Provider;

        public DependencyTest(ServiceProviderFixture fixture)
        {
            _providerFixture = fixture;
        }

        /// <summary>
        /// The service provider should be able to resolve a Game Generator.
        /// </summary>
        [Fact(DisplayName = "Resolve Game Generator")]
        public void ResolveGameGenerator()
        {
            var gameGen = Provider.GetService<IModelGenerator<Game>>();

            gameGen.Should()
                .NotBeNull("should have been initialized by the provider").And
                .BeOfType<GameGenerator>("registered as such by the AddGenerators extension").And
                .BeAssignableTo<IModelGenerator<Game>>("implements the IModelGenerator<T> interface");
        }

        /// <summary>
        /// The service provider should be able to resolve a Review Generator.
        /// </summary>
        [Fact(DisplayName = "Resolve Review Generator")]
        public void ResolveReviewGenerator()
        {
            var reviewGen = Provider.GetService<IModelGenerator<Review>>();

            reviewGen.Should()
                .NotBeNull("should have been initialized by the provider").And
                .BeOfType<ReviewGenerator>("registered as such by the AddGenerators extension").And
                .BeAssignableTo<IModelGenerator<Review>>("implements the IModelGenerator<T> interface");
        }

        /// <summary>
        /// The service provider should be able to resolve a Game Repository.
        /// </summary>
        [Fact(DisplayName = "Resolve Game Repository")]
        public void ResolveGameRepository()
        {
            var gameRepo = Provider.GetService<IGameRepository>();

            gameRepo.Should()
                .NotBeNull("should have been initialized by the provider").And
                .BeOfType<GameRepository>("registered as such by the AddUnitOfWork extension").And
                .BeAssignableTo<IGameRepository>("implements the IGameRepository interface");
        }

        /// <summary>
        /// The service provider should be able to resolve a Review Repository.
        /// </summary>
        [Fact(DisplayName = "Resolve Review Repository")]
        public void ResolveReviewRepository()
        {
            var reviewRepo = Provider.GetService<IReviewRepository>();

            reviewRepo.Should()
                .NotBeNull("should have been initialized by the provider").And
                .BeOfType<ReviewRepository>("registered as such by the AddUnitOfWork extension").And
                .BeAssignableTo<IReviewRepository>("implements the IReviewRepository interface");
        }

        /// <summary>
        /// The service provider should be able to resolve a Unit Of Work.
        /// </summary>
        [Fact(DisplayName = "Resolve Unit Of Work")]
        public void ResolveUnitOfWork()
        {
            var uow = Provider.GetService<IUnitOfWork>();

            uow.Should()
                .NotBeNull("should have been initialized by the provider").And
                .BeOfType<UnitOfWork>("registered as such by the AddUnitOfWork extension").And
                .BeAssignableTo<IUnitOfWork>("implements the IUnitOfWork interface");
        }
    }
}
