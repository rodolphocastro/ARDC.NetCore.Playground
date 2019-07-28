using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;
using ARDC.NetCore.Playground.API.ViewModels.Registration;
using ARDC.NetCore.Playground.Domain.Models;
using AutoMapper;
using FluentAssertions;
using System;
using Xunit;

namespace ARDC.NetCore.Playground.API.ViewModels.Tests
{
    public class GameViewModelsTest
    {
        private readonly IMapper _mapper;

        public GameViewModelsTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(ProfileRegistration.GetProfiles()));
            _mapper = mapperConfig.CreateMapper();
        }

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

        /// <summary>
        /// It should be possible to map from a CreateViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from Create")]
        public void MapFromCreate()
        {
            var newGame = new GameCreate
            {
                Name = "Pudim",
                ReleasedOn = DateTime.Now
            };

            var mappedGame = _mapper.Map<Game>(newGame);
            mappedGame.Should()
                .NotBeNull("a result is expected").And
                .BeOfType<Game>("it should be a Game class").And
                .BeEquivalentTo(newGame, "its properties should be the same as the original object");
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
                Name = "Game 123",
                ReleasedOn = DateTime.Now
            };

            Action act = new Action(() => _mapper.Map<GameCreate>(game));
            act.Should().Throw<Exception>();
        }

        /// <summary>
        /// It should be possible to map from a EditViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from Edit")]
        public void MapFromEdit()
        {
            var editGame = new GameEdit
            {
                Name = "Edited Game",
                ReleasedOn = DateTime.Now
            };

            var game = _mapper.Map<Game>(editGame);
            game.Should()
                .NotBeNull("an object is always expected").And
                .BeOfType<Game>("it should be a Game").And
                .BeEquivalentTo(editGame, "it should have the same properties as the game we mapped from");
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
                Name = "Existing Game",
                ReleasedOn = DateTime.Now
            };

            var editGame = _mapper.Map<GameEdit>(game);
            editGame.Should()
                .NotBeNull("").And
                .BeOfType<GameEdit>("").And
                .BeEquivalentTo(game, options => options.ExcludingMissingMembers(), because: "");
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
                Name = "Existing Game",
                ReleasedOn = DateTime.Now
            };

            var mappedGame = _mapper.Map<GameList>(game);
            mappedGame.Should()
                .NotBeNull("an object is always expected").And
                .BeOfType<GameList>("it should be a ListViewModel for Games").And
                .BeEquivalentTo(game, config => config.ExcludingMissingMembers(), because: "it should contain the same Properties as the original game");
        }

        /// <summary>
        /// It shouldnt be possible to map from a ListViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from List")]
        public void NotMapFromList()
        {
            var game = new GameList
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Existing Game"                
            };

            Action act = new Action(() => _mapper.Map<Game>(game));
            act.Should().Throw<Exception>(because: "no such mapping was registered");            
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
                Name = "Existing Game",
                ReleasedOn = DateTime.Now
            };

            var viewGame = _mapper.Map<GameView>(game);
            viewGame.Should()
                .NotBeNull("an object is always expected").And
                .BeOfType<GameView>("it should be a ViewViewModel").And
                .BeEquivalentTo(game, cfg => cfg.ExcludingMissingMembers(), because: "it should have the same properties as the original game");
        }

        /// <summary>
        /// It shouldnt be possible to map from a ViewViewModel to a Model.
        /// </summary>
        [Fact(DisplayName = "Map from View")]
        public void NotMapFromView()
        {
            var game = new GameView
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Existing Game",
                ReleasedOn = DateTime.Now                
            };

            Action act = new Action(() => _mapper.Map<Game>(game));
            act.Should().Throw<Exception>(because: "no such mapping was registered");
            
        }
    }
}
