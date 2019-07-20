using ARDC.NetCore.Playground.API.Features.Reviews;
using ARDC.NetCore.Playground.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    public class ReviewTest
    {
        private readonly ReviewController _controller;

        public ReviewTest()
        {
            _controller = new ReviewController();
        }

        /// <summary>
        /// It should be possible to get all the reviews.
        /// </summary>
        [Fact(DisplayName = "Get many Reviews")]
        public void GetReviews()
        {
            var result = _controller.Get();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an Ok Result").Which
                .Value.Should()
                    .NotBeNull("a object is always expected").And
                    .BeAssignableTo<IList<Review>>("it should be an implementation of IList<T>").And
                    .BeOfType<List<Review>>("it should be a List of Reviews, even if empty");
        }

        /// <summary>
        /// It should be possible to get a single review.
        /// </summary>
        [Fact(DisplayName = "Get a Review")]
        public void GetReview()
        {
            var result = _controller.Get("reviewId");

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK Result").Which
                .Value.Should()
                    .NotBeNull("an object is always expected").And
                    .BeOfType<Review>("it should be a Review").Which
                    .Id.Should().Be("reviewId", "it should be the review we searched for");
        }

        /// <summary>
        /// It should be possible to create a review.
        /// </summary>
        [Fact(DisplayName = "Create a Review")]
        public void CreateReview()
        {
            var newReview = new Review
            {
                AuthorName = "Pudim da Silva",
                ReviewText = "Texto da Review",
                Score = 6d,
                Subject = new Game
                {
                    Id = "pudimofwar",
                    Name = "Pudim of War",
                    ReleasedOn = DateTime.Now
                }
            };

            var result = _controller.Create(newReview);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("should implement IActionResult").And
                .BeOfType<CreatedResult>("it should be a Created result").Which
                .Value.Should()
                    .NotBeNull("a object is always excepcted").And
                    .BeOfType<Review>("it should be the review we just sent").And
                    .BeEquivalentTo(newReview, o => o.ExcludingMissingMembers(), "it should be equal to newReview, excepted for the ID");
        }

        /// <summary>
        /// It should be possible to update a review.
        /// </summary>
        [Fact(DisplayName = "Update a Review")]
        public void UpdateReview()
        {
            var review = new Review
            {
                Id = "reviewId",
                AuthorName = "Pudim da Silva",
                ReviewText = "Texto da Review",
                Score = 6d,
                Subject = new Game
                {
                    Id = "pudimofwar",
                    Name = "Pudim of War",
                    ReleasedOn = DateTime.Now
                }
            };

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
            var result = _controller.Delete("reviewId");

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<NoContentResult>("it should be a NoContent result");
        }
    }
}
