using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;
using ARDC.NetCore.Playground.API.ViewModels.Registration;
using ARDC.NetCore.Playground.API.ViewModels.ReviewViewModels;
using ARDC.NetCore.Playground.Domain.Models;
using AutoMapper;
using FluentAssertions;
using System;
using Xunit;

namespace ARDC.NetCore.Playground.API.ViewModels.Tests
{
    public class ReviewViewModelsTest
    {
        private readonly IMapper _mapper;

        public ReviewViewModelsTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(ProfileRegistration.GetProfiles()));
            _mapper = mapperConfig.CreateMapper();
        }

        /// <summary>
        /// A new review should have the correct properties.
        /// </summary>
        [Fact(DisplayName = "Create Properties")]
        public void CreateProperties()
        {
            typeof(ReviewCreate).Should()
                .NotHaveProperty("Id", "the id should only be generated after creation").And
                .HaveProperty<string>("AuthorName", "a review should contain its author's name").And
                .HaveProperty<string>("ReviewText", "a review should contain a text").And
                .HaveProperty<double>("Score", "a review should contain a score").And
                .HaveProperty<string>("SubjectId", "a review should be about a Game, know as subject, and hold its id").And
                .NotHaveProperty("Subject", "a review should contain only the game's Id, no the game itself");
        }

        /// <summary>
        /// A review being edited should have the correct properties
        /// </summary>
        [Fact(DisplayName = "Edit Properties")]
        public void EditProperties()
        {
            typeof(ReviewEdit).Should()
                .NotHaveProperty("Id", "the id shouldnt be editable").And
                .NotHaveProperty("SubjectId", "the subject's reference shouldnt be editable").And
                .NotHaveProperty("Subject", "the subject itself shouldnt be editable").And
                .NotHaveProperty("AuthorName", "the authors name shouldnt be editable").And
                .HaveProperty<string>("ReviewText", "the reviews text should be editable").And
                .HaveProperty<double>("Score", "the reviews score should be editable");
        }

        /// <summary>
        /// A review being edited should have the correct properties
        /// </summary>
        [Fact(DisplayName = "List Properties")]
        public void ListProperties()
        {
            typeof(ReviewList).Should()
                .HaveProperty<string>("Id", "the id should be available for further navigation").And
                .HaveProperty<string>("SubjectName", "the reviewed games name should be available to the user").And
                .HaveProperty<double>("Score", "the review score should be available to the user");
        }

        /// <summary>
        /// A review being edited should have the correct properties
        /// </summary>
        [Fact(DisplayName = "View Properties")]
        public void ViewProperties()
        {
            typeof(ReviewView).Should()
                .HaveProperty<string>("Id", "the id should be available for further navigation").And
                .HaveProperty<string>("AuthorName", "the reviews author name should be presented to the user").And
                .HaveProperty<string>("ReviewText", "the reviewed games name should be presented to the user").And
                .HaveProperty<double>("Score", "the review score should be presented to the user").And
                .HaveProperty<GameList>("Subject", "the subject should be presented to the user");
        }

        /// <summary>
        /// It should be possible to map from a CreateViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from Create")]
        public void MapFromCreate()
        {
            var newReview = new ReviewCreate
            {
                AuthorName = "Rodolpho C. Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 10d,
                SubjectId = Guid.NewGuid().ToString()
            };

            var review = _mapper.Map<Review>(newReview);
            review.Should()
                .NotBeNull(because: "an object is always expected").And
                .BeOfType<Review>(because: "it should be a Review").And
                .BeEquivalentTo(newReview, because: "it should have the same properties as the original object");
        }

        /// <summary>
        /// It shouldnt be possible to map to a CreateViewModel from a Model.
        /// </summary>
        [Fact(DisplayName = "Map to Create")]
        public void NotMapToCreate()
        {
            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Lorem of Ipsum",
                ReleasedOn = DateTime.Now
            };

            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                AuthorName = "Rodolpho C. Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 10d,
                Subject = game,
                SubjectId = game.Id
            };

            Action act = new Action(() => _mapper.Map<ReviewCreate>(review));
            act.Should().Throw<Exception>(because: "no such mapping should be registered");
        }

        /// <summary>
        /// It should be possible to map from an EditViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from Edit")]
        public void MapFromEdit()
        {
            var editReview = new ReviewEdit
            {
                ReviewText = "Pudim de Coco",
                Score = 6d
            };

            var review = _mapper.Map<Review>(editReview);
            review.Should()
                .NotBeNull(because: "an object is always expected").And
                .BeOfType<Review>(because: "it should be a Review").And
                .BeEquivalentTo(editReview, because: "it should have the same properties as the original review");
        }

        /// <summary>
        /// It should be possible to map to an EditViewModel from a Model.
        /// </summary>
        [Fact(DisplayName = "Map to Edit")]
        public void MapToEdit()
        {
            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Lorem of Ipsum",
                ReleasedOn = DateTime.Now
            };

            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                AuthorName = "Rodolpho C. Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 10d,
                Subject = game,
                SubjectId = game.Id
            };

            var editReview = _mapper.Map<ReviewEdit>(review);
            editReview.Should()
                .NotBeNull(because: "").And
                .BeOfType<ReviewEdit>(because: "").And
                .BeEquivalentTo(review, cfg => cfg.ExcludingMissingMembers());
        }

        /// <summary>
        /// It should be possible to map to a ListViewModel from a Model.
        /// </summary>
        [Fact(DisplayName = "Map to List")]
        public void MapToList()
        {
            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Lorem of Ipsum",
                ReleasedOn = DateTime.Now
            };

            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                AuthorName = "Rodolpho C. Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 10d,
                Subject = game,
                SubjectId = game.Id
            };

            var listView = _mapper.Map<ReviewList>(review);
            listView.Should()
                .NotBeNull(because: "an object is always expected").And
                .BeOfType<ReviewList>(because: "it should be a ReviewList").And
                .BeEquivalentTo(review, cfg => cfg.ExcludingMissingMembers(), because: "it should contain the same properties as the original Review");

            listView.SubjectName.Should().Be(game.Name, because: "the games name should be flattened");
        }

        /// <summary>
        /// It shouldnt be possible to map from a ListViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from List")]
        public void NotMapFromList()
        {
            var listReview = new ReviewList
            {
                Id = Guid.NewGuid().ToString(),
                SubjectName = "Lorem of Ipsum",
                Score = 6d
            };

            Action act = new Action(() => _mapper.Map<Review>(listReview));
            act.Should().Throw<Exception>(because: "no such mapping should be registered");
        }

        /// <summary>
        /// It should be possible to map to a ViewViewModel from a Model.
        /// </summary>
        [Fact(DisplayName = "Map to View")]
        public void MapToView()
        {
            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Lorem of Ipsum",
                ReleasedOn = DateTime.Now
            };

            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                AuthorName = "Rodolpho C. Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 10d,
                Subject = game,
                SubjectId = game.Id
            };

            var reviewView = _mapper.Map<ReviewView>(review);
            reviewView.Should()
                .NotBeNull(because: "").And
                .BeOfType<ReviewView>(because: "").And
                .BeEquivalentTo(review, cfg => cfg.ExcludingMissingMembers(), because: "");

            reviewView.Subject.Should().BeEquivalentTo(game, cfg => cfg.ExcludingMissingMembers(), because: "");
        }

        /// <summary>
        /// It shouldnt be possible to map from a ViewViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "")]
        public void NotMapFromView()
        {
            var reviewView = new ReviewView
            {
                Id = Guid.NewGuid().ToString(),
                AuthorName = "Rodolpho C Alves",
                ReviewText = "Lorem ipsum dolor sit amet",
                Score = 6d,
                Subject = new GameList
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Lorem of Ipsum"
                }
            };

            Action act = new Action(() => _mapper.Map<Review>(reviewView));
            act.Should().Throw<Exception>(because: "no such mapping should exist");
        }
    }
}
