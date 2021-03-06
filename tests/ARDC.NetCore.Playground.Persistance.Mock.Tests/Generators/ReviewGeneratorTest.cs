﻿using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Mock.Tests.Generators
{
    /// <summary>
    /// Tests for the ReviewGenerator.
    /// </summary>
    public class ReviewGeneratorTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly IModelGenerator<Review> _reviewGenerator;

        public ReviewGeneratorTest(ServiceProviderFixture fixture)
        {
            _reviewGenerator = fixture.Provider.GetService<IModelGenerator<Review>>();
        }

        /// <summary>
        /// It should be possible to create a single review.
        /// </summary>
        [Fact(DisplayName = "Create Single Review")]
        public void CreateSingle()
        {
            var review = _reviewGenerator.Get();

            review.Should()
                .NotBeNull("should have been generated as a whole").And
                .NotBeOfType<IList<Review>>("should be a single object").And
                .BeOfType<Review>("should be a Review object");

            review.Id.Should().NotBeNullOrWhiteSpace("created from a Guid");
            review.AuthorName.Should().NotBeNullOrWhiteSpace("created by Bogus");
            review.ReviewText.Should().NotBeNullOrWhiteSpace("created by Bogus");
            review.Score.Should()
                .BeGreaterOrEqualTo(0, "generated within 0 and 10").And
                .BeLessOrEqualTo(10, "generated within 0 and 10");
            review.Subject.Should()
                .NotBeNull("should have been generated by the GameGenerator");
        }

        /// <summary>
        /// It should be possible to generate a set number of reviews.
        /// </summary>
        /// <param name="count">Amount of reviews to be generated</param>
        [Theory(DisplayName = "Create Many Reviews")]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(200)]
        [InlineData(15)]
        public void CreateMany(int count)
        {
            var reviews = _reviewGenerator.Get(count);  // TODO: Tratar quantidades negativas e zero

            reviews.Should()
                .NotBeNull("should have been generated as a whole").And
                .BeAssignableTo<IList<Review>>("should implement the IList<T> interface").And
                .BeOfType<List<Review>>("should be created as a List<T> by Faker").And
                .NotBeEmpty("IModelGenerator<T> doesn't create empty collections").And
                .HaveCount(count, "should respect the count param");
        }

        /// <summary>
        /// It should be possible to generate a number of reviews within a Range.
        /// </summary>
        /// <param name="min">The minimum amount</param>
        /// <param name="max">The maximum amount</param>
        [Theory(DisplayName = "Create a Range of Reviews")]
        [InlineData(2, 10)]
        [InlineData(20, 20)]
        [InlineData(200, 201)]
        [InlineData(15, 30)]
        public void CreateRange(int min, int max)
        {
            var reviews = _reviewGenerator.Get(min, max);  // TODO: Tratar quantidades negativas, zero e min maior que max.

            reviews.Should()
                .NotBeNull("should have been generated as a whole").And
                .BeAssignableTo<IList<Review>>("should implement the IList<T> interface").And
                .BeOfType<List<Review>>("should be created as a List<T> by Faker").And
                .NotBeEmpty("IModelGenerator<T> doesn't create empty collections").And
                .HaveCountGreaterOrEqualTo(min, "should respect the min param").And
                .HaveCountLessOrEqualTo(max, "should respect the max param");
        }
    }
}
