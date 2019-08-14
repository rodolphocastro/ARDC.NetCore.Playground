using ARDC.NetCore.Playground.Domain;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ARDC.NetCore.Playground.Persistence.Core.Tests
{
    public class UnitOfWorkTests : IClassFixture<ServiceProviderFixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkTests(ServiceProviderFixture fixture)
        {
            _unitOfWork = fixture.Provider.GetService<IUnitOfWork>();
        }

        /// <summary>
        /// It should be possible to SaveChanges
        /// </summary>
        [Fact(DisplayName = "Save Changes")]
        public void SaveChanges()
        {
            Action act = new Action(() => _unitOfWork.SaveChanges());
            act.Should().NotThrow<Exception>("it should be possible to commit without issues");
        }

        /// <summary>
        /// It should be possible to SaveChanges
        /// </summary>
        [Fact(DisplayName = "Save Changes (Async)")]
        public void SaveChangesAsync()
        {
            Func<Task> act = new Func<Task>(() => _unitOfWork.SaveChangesAsync());
            act.Should().NotThrow<Exception>("it should be possible to commit without issues");
        }
    }
}
