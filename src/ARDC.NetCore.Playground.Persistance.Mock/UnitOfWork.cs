using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistence.Mock.Generators;
using ARDC.NetCore.Playground.Persistence.Mock.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistence.Mock
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGameRepository _gameRepository;
        private IReviewRepository _reviewRepository;
        private readonly IModelGenerator<Game> _gameGenerator;
        private readonly IModelGenerator<Review> _reviewGenerator;

        public UnitOfWork(IModelGenerator<Game> gameGenerator, IModelGenerator<Review> reviewGenerator)
        {
            _gameGenerator = gameGenerator ?? throw new ArgumentNullException(nameof(gameGenerator));
            _reviewGenerator = reviewGenerator ?? throw new ArgumentNullException(nameof(reviewGenerator));
        }

        public IGameRepository GameRepository
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GameRepository(_gameGenerator);
                return _gameRepository;
            }
        }

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (_reviewRepository == null)
                    _reviewRepository = new ReviewRepository(_reviewGenerator);
                return _reviewRepository;
            }
        }

        public void SaveChanges() { }

        public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;
    }
}
