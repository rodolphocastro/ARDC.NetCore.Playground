using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistance.Mock
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;

        public UnitOfWork(IGameRepository gameRepository, IReviewRepository reviewRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        public IGameRepository GameRepository => _gameRepository;

        public IReviewRepository ReviewRepository => _reviewRepository;

        public void SaveChanges() { }

        public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;
    }
}
