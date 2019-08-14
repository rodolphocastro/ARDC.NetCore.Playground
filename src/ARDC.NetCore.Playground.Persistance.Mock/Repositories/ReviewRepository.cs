using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistence.Mock.Generators;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistence.Mock.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IModelGenerator<Review> _reviewGenerator;

        public ReviewRepository(IModelGenerator<Review> reviewGenerator)
        {
            _reviewGenerator = reviewGenerator ?? throw new ArgumentNullException(nameof(reviewGenerator));
        }

        public Review Create(Review review)
        {
            if (string.IsNullOrWhiteSpace(review.Id))
                review.Id = Guid.NewGuid().ToString();

            return review;
        }

        public Task<Review> CreateAsync(Review review, CancellationToken ct) => Task.FromResult(Create(review));

        public void Delete(string id) { }

        public void Delete(Review review) { }

        public Task DeleteAsync(string id, CancellationToken ct) => Task.CompletedTask;

        public Task DeleteAsync(Review review, CancellationToken ct) => Task.CompletedTask;

        public Review Get(string id)
        {
            var review = _reviewGenerator.Get();
            review.Id = id;

            return review;
        }

        public IList<Review> Get() => _reviewGenerator.Get(2, 200);

        public Task<Review> GetAsync(string id, CancellationToken ct) => Task.FromResult(Get(id));

        public Task<IList<Review>> GetAsync(CancellationToken ct) => Task.FromResult(Get());

        public void Update(string id, Review review) { }

        public Task UpdateAsync(string id, Review review, CancellationToken ct) => Task.CompletedTask;
    }
}
