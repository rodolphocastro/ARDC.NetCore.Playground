using ARDC.NetCore.Playground.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Domain.Repository
{
    /// <summary>
    /// Describe methods for accessing Reviews in the System.
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Get a single review.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="ct">Token for Task cancellation</param>
        /// <returns>The review found for the given ID</returns>
        Task<Review> GetAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get a single review.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <returns>The review found for the given ID</returns>
        Review Get(string id);

        /// <summary>
        /// Get all the reviews.
        /// </summary>
        /// <param name="ct">Token for Task cancellation</param>
        /// <returns>A list of reviews</returns>
        Task<IList<Review>> GetAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all the reviews.
        /// </summary>
        /// <returns>A list of reviews</returns>
        IList<Review> Get();

        /// <summary>
        /// Create a new review.
        /// </summary>
        /// <param name="review">The review to be created</param>
        /// <param name="ct">Token for Task cancellation</param>
        /// <returns>The review stored in the system</returns>
        Task<Review> CreateAsync(Review review, CancellationToken ct = default);

        /// <summary>
        /// Create a new review.
        /// </summary>
        /// <param name="review">The review to be created</param>
        /// <returns>The review stored in the system</returns>
        Review Create(Review review);

        /// <summary>
        /// Update an existing review.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="review">The review's updated data</param>
        /// <param name="ct">Token for Task cancellation</param>
        Task UpdateAsync(string id, Review review, CancellationToken ct = default);

        /// <summary>
        /// Update an existing review.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="review">The review's updated data</param>
        void Update(string id, Review review);

        /// <summary>
        /// Delete an existing review.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="ct">Token for Task cancellation</param>
        Task DeleteAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Delete an existing review.
        /// </summary>
        /// <param name="review">The review that should be deleted</param>
        /// <param name="ct">Token for Task cancellation</param>
        Task DeleteAsync(Review review, CancellationToken ct = default);

        /// <summary>
        /// Delete an existing review.
        /// </summary>
        /// <param name="id">The review's id</param>
        void Delete(string id);

        /// <summary>
        /// Delete an existing review.
        /// </summary>
        /// <param name="review">The review that should be deleted</param>
        void Delete(Review review);
    }
}
