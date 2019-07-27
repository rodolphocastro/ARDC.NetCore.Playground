using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistance.Memory.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PlaygroundContext _context;

        public ReviewRepository(PlaygroundContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Review Create(Review review)
        {
            var newReview = _context.Reviews.Add(review);
            return newReview.Entity;
        }

        public Task<Review> CreateAsync(Review review, CancellationToken ct) => Task.FromResult(Create(review));

        public void Delete(string id)
        {
            var review = _context.Reviews.Where(r => r.Id == id).SingleOrDefault();
            Delete(review);
        }

        public void Delete(Review review) => _context.Reviews.Remove(review);

        public async Task DeleteAsync(string id, CancellationToken ct)
        {
            var review = await _context.Reviews.Where(r => r.Id == id).SingleOrDefaultAsync();
            await DeleteAsync(review, ct);
        }

        public Task DeleteAsync(Review review, CancellationToken ct) => Task.FromResult(_context.Reviews.Remove(review));

        public Review Get(string id) => _context.Reviews.Where(r => r.Id == id).SingleOrDefault();

        public IList<Review> Get() => _context.Reviews.ToList();

        public async Task<Review> GetAsync(string id, CancellationToken ct) => await _context.Reviews.Where(r => r.Id == id).SingleOrDefaultAsync(ct);

        public async Task<IList<Review>> GetAsync(CancellationToken ct) => await _context.Reviews.ToListAsync(ct);

        public void Update(string id, Review review) => _context.Entry(review).State = EntityState.Modified;

        public Task UpdateAsync(string id, Review review, CancellationToken ct)
        {
            Update(id, review);
            return Task.CompletedTask;
        }
    }
}
