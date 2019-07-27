using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace ARDC.NetCore.Playground.API.ViewModels.Tests
{
    public class GameViewModelsTest
    {
        /// <summary>
        /// A new game should have the correct properties.
        /// </summary>
        [Fact(DisplayName = "Create Properties")]
        public void CreateProperties()
        {
            typeof(GameCreate).Should()
                .NotHaveProperty("Id", "the id should only be generated after creation").And
                .HaveProperty<string>("Name", "all games require a name").And
                .HaveProperty<DateTime>("ReleasedOn", "all games require a release date");
        }

        /// <summary>
        /// A game being edited should have the correct properties.
        /// </summary>
        [Fact(DisplayName = "Edit Properties")]
        public void EditProperties()
        {
            typeof(GameEdit).Should()
                .NotHaveProperty("Id", "a games id cannot be edited").And
                .HaveProperty<string>("Name", "the game's name should be editable").And
                .HaveProperty<DateTime>("ReleasedOn", "the game's released date should be editable");
        }

        /// <summary>
        /// A game in a list should have the correct properties.
        /// </summary>
        [Fact(DisplayName = "List Properties")]
        public void ListProperties()
        {
            typeof(GameList).Should()
                .HaveProperty<string>("Id", "its id should be contained to allow further navigation").And
                .HaveProperty<string>("Name", "its name should be presented to the users").And
                .NotHaveProperty("ReleasedOn", "its ReleaseDate should not be presented");
        }

        /// <summary>
        /// A game being detailed should have the correct properties.
        /// </summary>
        [Fact(DisplayName = "View Properties")]
        public void ViewProperties()
        {
            typeof(GameView).Should()
                .HaveProperty<string>("Id", "its id should be contained to allow further navigation").And
                .HaveProperty<string>("Name", "its name should be presented to the users").And
                .HaveProperty<DateTime>("ReleasedOn", "its release date should be presented to the users");
        }
    }
}
