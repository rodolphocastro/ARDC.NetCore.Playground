using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Memory.Tests.Repositories
{
    public class ReviewRepositoryTests : IClassFixture<ServiceProviderFixture>
    {
        private readonly IModelGenerator<Review> _reviewGenerator;
        private readonly IReviewRepository _reviewRepository;
        private readonly PlaygroundContext _context;

        public ReviewRepositoryTests(ServiceProviderFixture fixture)
        {
            _reviewGenerator = fixture.Provider.GetService<IModelGenerator<Review>>();
            _reviewRepository = fixture.Provider.GetService<IUnitOfWork>().ReviewRepository;
            _context = fixture.Provider.GetService<PlaygroundContext>();
        }

        /// <summary>
        /// It should be possible to create a new review.
        /// </summary>
        [Fact(DisplayName = "Create a Review")]
        public void Create()
        {
            var newReview = _reviewGenerator.Get();
            var game = _context.Games.FirstOrDefault();
            newReview.Subject = game;
            newReview.SubjectId = game.Id;
            var reviewCount = _reviewRepository.Get().Count;

            var createdReview = _reviewRepository.Create(newReview);
            createdReview.Should()
                .NotBeNull("a new review is expected").And
                .BeAssignableTo<Review>("it should be a Review object").And
                .BeEquivalentTo(newReview, "it should have the same properties as the one we sent");

            _context.SaveChanges();

            _reviewRepository.Get().Count.Should().BeGreaterThan(reviewCount, "a new review shouldve been added to the collection");
        }

        /// <summary>
        /// It should be possible to create a new review.
        /// </summary>
        [Fact(DisplayName = "Create a Review (Async)")]
        public async Task CreateAsync()
        {
            var newReview = _reviewGenerator.Get();
            var game = await _context.Games.FirstOrDefaultAsync();
            newReview.Subject = game;
            newReview.SubjectId = game.Id;
            var reviewCount = (await _reviewRepository.GetAsync()).Count;

            var createdReview = await _reviewRepository.CreateAsync(newReview);
            createdReview.Should()
                .NotBeNull("a new review is expected").And
                .BeAssignableTo<Review>("it should be a Review object").And
                .BeEquivalentTo(newReview, "it should have the same properties as the one we sent");

            _context.SaveChanges();

            _reviewRepository.Get().Count.Should().BeGreaterThan(reviewCount, "a new reviews shouldve been added to the collection");
        }

        /// <summary>
        /// It should be possible to delete a review by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Review by id")]
        public void DeleteById()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            var reviewCount = _reviewRepository.Get().Count;

            Action act = new Action(() => _reviewRepository.Delete(firstReview.Id));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing review");
            _context.SaveChanges();

            _context.Reviews.Count().Should().BeLessThan(reviewCount, "a review shouldve been removed from the collection");
        }

        /// <summary>
        /// It should be possible to delete a review by its reference.
        /// </summary>
        [Fact(DisplayName = "Delete a Review by reference")]
        public void DeleteByObject()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            var reviewCount = _reviewRepository.Get().Count;

            Action act = new Action(() => _reviewRepository.Delete(firstReview));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing review");
            _context.SaveChanges();

            _context.Reviews.Count().Should().BeLessThan(reviewCount, "a review shouldve been removed from the collection");
        }

        /// <summary>
        /// It should be possible to delete a review by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Review by id (Async)")]
        public void DeleteByIdAsync()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            var reviewCount = _reviewRepository.Get().Count;

            Func<Task> act = new Func<Task>(() => _reviewRepository.DeleteAsync(firstReview.Id));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing review");
            _context.SaveChanges();

            _context.Reviews.Count().Should().BeLessThan(reviewCount, "a review shouldve been removed from the collection");
        }

        /// <summary>
        /// It should be possible to delete a review by its reference.
        /// </summary>
        [Fact(DisplayName = "Delete a Review by reference (Async)")]
        public void DeleteByObjectAsync()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            var reviewCount = _reviewRepository.Get().Count;

            Func<Task> act = new Func<Task>(() => _reviewRepository.DeleteAsync(firstReview));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing review");
            _context.SaveChanges();

            _context.Reviews.Count().Should().BeLessThan(reviewCount, "a review shouldve been removed from the collection");
        }

        /// <summary>
        /// It should be possible to recover a single review.
        /// </summary>
        [Fact(DisplayName = "Get a Review")]
        public void Get()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            var result = _reviewRepository.Get(firstReview.Id);

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<Review>("it should be a Review").And
                .BeEquivalentTo(firstReview, "it should be the same one we got from the context");
        }

        /// <summary>
        /// It should be possible to recover a single review.
        /// </summary>
        [Fact(DisplayName = "Get a Review (Async)")]
        public async Task GetAsync()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            var result = await _reviewRepository.GetAsync(firstReview.Id);

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<Review>("it should be a Review").And
                .BeEquivalentTo(firstReview, "it should be the same one we got from the context");
        }

        /// <summary>
        /// it should be possible to recover all reviews.
        /// </summary>
        [Fact(DisplayName = "Get all Reviews")]
        public void GetAll()
        {
            var reviews = _reviewRepository.Get();
            reviews.Should()
                .NotBeNull("a result is always expected").And
                .NotBeEmpty("it should contain at least a single element");
        }

        /// <summary>
        /// it should be possible to recover all reviews.
        /// </summary>
        [Fact(DisplayName = "Get all Reviews (Async)")]
        public async Task GetAllAsync()
        {
            var reviews = await _reviewRepository.GetAsync();
            reviews.Should()
                .NotBeNull("a result is always expected").And
                .NotBeEmpty("it should contain at least a single element");
        }

        /// <summary>
        /// It should be possible to update a review.
        /// </summary>
        [Fact(DisplayName = "Update a Review")]
        public void Update()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            firstReview.Score = 2;

            Action act = new Action(() => _reviewRepository.Update(firstReview.Id, firstReview));
            act.Should().NotThrow<Exception>("it should update without errors");
            _context.SaveChanges();
        }

        /// <summary>
        /// It should be possible to update a review.
        /// </summary>
        [Fact(DisplayName = "Update a Review (Async)")]
        public void UpdateAsync()
        {
            var firstReview = _context.Reviews.FirstOrDefault();
            firstReview.Score = 2;

            Func<Task> act = new Func<Task>(() => _reviewRepository.UpdateAsync(firstReview.Id, firstReview));
            act.Should().NotThrow<Exception>("it should update without errors");
            _context.SaveChanges();
        }
    }
}
