using ARDC.NetCore.Playground.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Domain
{
    /// <summary>
    /// Describe methods for dealing with the Repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Games' Repository.
        /// </summary>
        IGameRepository GameRepository { get; }

        /// <summary>
        /// Reviews' Repository.
        /// </summary>
        IReviewRepository ReviewRepository { get; }

        /// <summary>
        /// Save pending changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Save pending changes.
        /// </summary>
        /// <param name="ct">Token for task cancellation</param>
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
