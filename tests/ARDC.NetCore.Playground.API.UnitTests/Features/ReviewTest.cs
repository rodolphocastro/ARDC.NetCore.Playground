using ARDC.NetCore.Playground.API.Features.Reviews;
using ARDC.NetCore.Playground.API.ViewModels.Registration;
using ARDC.NetCore.Playground.API.ViewModels.ReviewViewModels;
using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IModelGenerator<Review> _reviewGenerator;
        private readonly Mock<IReviewRepository> _fakeRepository;
        private readonly Mock<IUnitOfWork> _fakeUnitOfWork;
        private readonly ReviewController _controller;

        public ReviewTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(ProfileRegistration.GetProfiles()));
            _mapper = mapperConfig.CreateMapper();
            _reviewGenerator = new ReviewGenerator(new GameGenerator());
            _fakeRepository = new Mock<IReviewRepository>();
            _fakeUnitOfWork = new Mock<IUnitOfWork>();
            _fakeUnitOfWork.Setup(m => m.ReviewRepository).Returns(_fakeRepository.Object);
            _controller = new ReviewController(_fakeUnitOfWork.Object, _mapper);
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
                    .BeAssignableTo<IList<ReviewList>>("it should be an implementation of IList<T>").And
                    .BeOfType<List<ReviewList>>("it should be a List of Reviews, even if empty");

            var reviews = (result as OkObjectResult).Value as List<ReviewList>;
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
                    .BeOfType<ReviewView>("it should be a Review");

            var returnedReview = (result as OkObjectResult).Value as ReviewView;
            returnedReview.Should()
                .BeEquivalentTo(newReview, opt => opt.ExcludingMissingMembers(), because: "it should be the same as the review we sent to the Controller");
            returnedReview.Subject.Should().BeEquivalentTo(newReview.Subject, cfg => cfg.ExcludingMissingMembers());
        }

        /// <summary>
        /// It should be possible to create a review.
        /// </summary>
        [Fact(DisplayName = "Create a Review")]
        public void CreateReview()
        {
            var newReview = new ReviewCreate
            {
                AuthorName = "Rodolpho Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 6d,
                SubjectId = Guid.NewGuid().ToString()
            };

            _fakeRepository.Setup(m => m.Create(It.IsAny<Review>())).Returns(() =>
            {
                return new Review
                {
                    Id = Guid.NewGuid().ToString(),
                    AuthorName = newReview.AuthorName,
                    ReviewText = newReview.ReviewText,
                    Score = newReview.Score,
                    SubjectId = newReview.SubjectId,
                    Subject = new Game
                    {
                        Id = newReview.SubjectId,
                        Name = "Lorem Ipsum",
                        ReleasedOn = DateTime.Now
                    }
                };
            });

            var result = _controller.Create(newReview);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("should implement IActionResult").And
                .BeOfType<CreatedResult>("it should be a Created result").Which
                .Value.Should()
                    .NotBeNull("a object is always excepcted").And
                    .BeOfType<ReviewView>("it should be the review we just sent");

            var createdReview = (result as CreatedResult).Value as ReviewView;
            createdReview.Should()
                .BeEquivalentTo(newReview, cfg => cfg.ExcludingMissingMembers(), because: "it should be equal to the one we sent to the controller");
        }

        /// <summary>
        /// It should be possible to update a review.
        /// </summary>
        [Fact(DisplayName = "Update a Review")]
        public void UpdateReview()
        {
            var reviewEdit = new ReviewEdit
            {
                ReviewText = "New Text",
                Score = 6d
            };

            _fakeRepository.Setup(m => m.Get("reviewId")).Returns(new Review
            {
                Id = "reviewId",
                AuthorName = "Author",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 10d,
                SubjectId = Guid.NewGuid().ToString(),
                Subject = new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "A Game",
                    ReleasedOn = DateTime.Now
                }
            });

            _fakeRepository.Setup(m => m.Update("reviewId", It.IsAny<Review>()));

            var result = _controller.Update("reviewId", reviewEdit);
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
