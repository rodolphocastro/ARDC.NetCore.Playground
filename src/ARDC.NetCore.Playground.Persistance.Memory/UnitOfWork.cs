using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Memory.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistance.Memory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlaygroundContext _context;
        private GameRepository gameRepository;
        private ReviewRepository reviewRepository;

        public UnitOfWork(PlaygroundContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGameRepository GameRepository
        {
            get
            {
                if (gameRepository == null)
                    gameRepository = new GameRepository(_context);
                return gameRepository;
            }
        }

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (reviewRepository == null)
                    reviewRepository = new ReviewRepository(_context);
                return reviewRepository;
            }
        }

        public void SaveChanges() => _context.SaveChanges();

        public Task SaveChangesAsync(CancellationToken ct) => _context.SaveChangesAsync(ct);
    }
}
