using ARDC.NetCore.Playground.API.ViewModels.Game;
using ARDC.NetCore.Playground.API.ViewModels.Review;
using FluentAssertions;
using Xunit;

namespace ARDC.NetCore.Playground.API.ViewModels.Tests
{
    public class ReviewViewModelsTest
    {
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
                .HaveProperty<GameList>("ReviewSubject", "the subject should be presented to the user");
        }
    }
}
