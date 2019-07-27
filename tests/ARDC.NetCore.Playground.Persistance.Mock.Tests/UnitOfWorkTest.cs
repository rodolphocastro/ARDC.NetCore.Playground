using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Mock.Tests
{
    /// <summary>
    /// Tests for the UnitOfWork Mock.
    /// </summary>
    public class UnitOfWorkTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkTest(ServiceProviderFixture fixture)
        {
            _unitOfWork = fixture.Provider.GetService<IUnitOfWork>();
        }

        /// <summary>
        /// It should be possible to get the UoW's Game Repository.
        /// </summary>
        [Fact(DisplayName = "Get Game Repository")]
        public void GetGameRepository()
        {
            var gameRepo = _unitOfWork.GameRepository;

            gameRepo.Should()
                .NotBeNull("should have been created by the IOC").And
                .BeAssignableTo<IGameRepository>("be an implementation of IGameRepository").And
                .BeOfType<GameRepository>("use the GameRepository class");
        }

        /// <summary>
        /// It should be possible to get the UoW's Review Repository.
        /// </summary>
        [Fact(DisplayName = "Get Review Repository")]
        public void GetReviewRepository()
        {
            var reviewRepo = _unitOfWork.ReviewRepository;

            reviewRepo.Should()
                .NotBeNull("should have been created by the IOC").And
                .BeAssignableTo<IReviewRepository>("be an implementation of IReviewRepository").And
                .BeOfType<ReviewRepository>("use the ReviewRepository class");
        }

        /// <summary>
        /// It should be possible to Save Changes.
        /// </summary>
        [Fact(DisplayName = "Save Changes")]
        public void SaveChanges()
        {
            Action act = () => _unitOfWork.SaveChanges();
            act.Should().NotThrow<Exception>("it should do nothing");
        }

        /// <summary>
        /// It should be possible to Save Changes.
        /// </summary>
        [Fact(DisplayName = "Save Changes Async")]
        public void SaveChangesAsync()
        {
            Func<Task> act = async () => await _unitOfWork.SaveChangesAsync();
            act.Should().NotThrow<Exception>("it should do nothing");
        }
    }
}
