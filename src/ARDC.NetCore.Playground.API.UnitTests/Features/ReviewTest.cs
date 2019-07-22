using ARDC.NetCore.Playground.API.Features.Reviews;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    public class ReviewTest
    {
        private readonly IModelGenerator<Review> _reviewGenerator;
        private readonly Mock<IReviewRepository> _fakeRepository;
        private readonly ReviewController _controller;

        public ReviewTest()
        {
            _reviewGenerator = new ReviewGenerator(new GameGenerator());
            _fakeRepository = new Mock<IReviewRepository>();
            _controller = new ReviewController(_fakeRepository.Object);
        }

        /// <summary>
        /// It should be possible to get all the reviews.
        /// </summary>
        [Theory(DisplayName = "Get many Reviews")]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(5)]
        public void GetReviews(int count)
        {
            _fakeRepository.Setup(m => m.Get()).Returns(_reviewGenerator.Get(count));
            var result = _controller.Get();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an Ok Result").Which
                .Value.Should()
                    .NotBeNull("a object is always expected").And
                    .BeAssignableTo<IList<Review>>("it should be an implementation of IList<T>").And
                    .BeOfType<List<Review>>("it should be a List of Reviews, even if empty");

            var reviews = (result as OkObjectResult).Value as List<Review>;
            reviews.Should()
                .NotBeEmpty("its repository has elements").And
                .HaveCount(count, $"its repository has {count} elements");
        }

        /// <summary>
        /// It should be possible to get a single review.
        /// </summary>
        [Fact(DisplayName = "Get a Review")]
        public void GetReview()
        {
            var newReview = _reviewGenerator.Get();
            _fakeRepository.Setup(m => m.Get(newReview.Id)).Returns(newReview);
            var result = _controller.Get(newReview.Id);

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK Result").Which
                .Value.Should()
                    .NotBeNull("an object is always expected").And
                    .BeOfType<Review>("it should be a Review");

            var returnedReview = (result as OkObjectResult).Value as Review;
            returnedReview.Should()
                .BeEquivalentTo(newReview, "it should be the same as the review we sent to the Controller");

        }

        /// <summary>
        /// It should be possible to create a review.
        /// </summary>
        [Fact(DisplayName = "Create a Review")]
        public void CreateReview()
        {
            var newReview = _reviewGenerator.Get();
            newReview.Id = string.Empty;
            _fakeRepository.Setup(m => m.Create(newReview)).Returns(() =>
            {
                newReview.Id = Guid.NewGuid().ToString();
                return newReview;
            });

            var result = _controller.Create(newReview);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("should implement IActionResult").And
                .BeOfType<CreatedResult>("it should be a Created result").Which
                .Value.Should()
                    .NotBeNull("a object is always excepcted").And
                    .BeOfType<Review>("it should be the review we just sent");

            var createdReview = (result as CreatedResult).Value as Review;
            createdReview.Should()
                .BeEquivalentTo(newReview, "it should be equal to the one we sent to the controller");
        }

        /// <summary>
        /// It shouldn't be possible to create an existing review.
        /// </summary>
        [Fact(DisplayName = "Create an existing Review")]
        public void CreateExistingReview()
        {
            var newReview = _reviewGenerator.Get();
            _fakeRepository.Setup(m => m.Create(newReview)).Throws<Exception>();            

            var result = _controller.Create(newReview);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("should implement IActionResult").And
                .BeOfType<BadRequestResult>("it should be a Bad Request");                
        }

        /// <summary>
        /// It should be possible to update a review.
        /// </summary>
        [Fact(DisplayName = "Update a Review")]
        public void UpdateReview()
        {
            var review = _reviewGenerator.Get();
            _fakeRepository.Setup(m => m.Update(review.Id, review));

            var result = _controller.Update(review.Id, review);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkResult>("it should be an OK result");
        }

        /// <summary>
        /// It should be possible to delete a review.
        /// </summary>
        [Fact(DisplayName = "Delete a Review")]
        public void DeleteReview()
        {
            var review = _reviewGenerator.Get();
            _fakeRepository.Setup(m => m.Delete(review.Id));

            var result = _controller.Delete(review.Id);

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<NoContentResult>("it should be a NoContent result");
        }
    }
}
