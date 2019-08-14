using ARDC.NetCore.Playground.Domain.Models;
using Bogus;
using System;
using System.Collections.Generic;

namespace ARDC.NetCore.Playground.Persistence.Mock.Generators
{
    /// <summary>
    /// Mock data generator for games.
    /// </summary>
    public class GameGenerator : IModelGenerator<Game>
    {
        private readonly Random _random;
        private readonly Faker<Game> _faker;

        public GameGenerator()
        {
            _random = new Random();

            _faker = new Faker<Game>("pt_BR")
                .RuleFor(g => g.Id, f => f.Random.Guid().ToString())
                .RuleFor(g => g.Name, f => f.Commerce.Product())
                .RuleFor(g => g.ReleasedOn, f => f.Date.Past(10));
        }

        public Game Get() => _faker.Generate();

        public IList<Game> Get(int count) => _faker.Generate(count);

        public IList<Game> Get(int min, int max) => _faker.Generate(_random.Next(min, max));
    }
}
