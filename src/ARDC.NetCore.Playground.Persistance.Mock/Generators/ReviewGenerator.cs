using ARDC.NetCore.Playground.Domain.Models;
using Bogus;
using System;
using System.Collections.Generic;

namespace ARDC.NetCore.Playground.Persistance.Mock.Generators
{
    /// <summary>
    /// Mock data generator for Reviews.
    /// </summary>
    public class ReviewGenerator : IModelGenerator<Review>
    {
        private readonly Random _random;
        private readonly Faker<Review> _faker;
        private readonly IModelGenerator<Game> _gameGenerator;

        public ReviewGenerator(IModelGenerator<Game> gameGenerator)
        {
            _gameGenerator = gameGenerator ?? throw new ArgumentNullException(nameof(gameGenerator));
            _random = new Random();

            _faker = new Faker<Review>("pt_BR")
                .RuleFor(r => r.Id, f => f.Random.Guid().ToString())
                .RuleFor(r => r.AuthorName, f => f.Person.FullName)
                .RuleFor(r => r.Subject, f => _gameGenerator.Get())
                .RuleFor(r => r.Score, f => f.Random.Double(0, 10))
                .RuleFor(r => r.ReviewText, (f, u) => f.Rant.Review(u.Subject.Name))
                .RuleFor(r => r.SubjectId, (f, u) => u.Subject.Id);
        }

        public Review Get() => _faker.Generate();

        public IList<Review> Get(int count) => _faker.Generate(count);

        public IList<Review> Get(int min, int max) => _faker.Generate(_random.Next(min, max));
    }
}
