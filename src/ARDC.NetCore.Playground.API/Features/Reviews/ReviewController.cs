using ARDC.NetCore.Playground.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ARDC.NetCore.Playground.API.Features.Reviews
{
    [Route("reviews")]
    public class ReviewController : ControllerBase
    {
        public ReviewController()
        {

        }

        /// <summary>
        /// Get all the reviews in the system,
        /// </summary>
        /// <returns>A list of reviews</returns>
        [HttpGet(Name = "GET Reviews")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Review>))]
        public IActionResult Get() => Ok(new List<Review>());

        /// <summary>
        /// Get a specific review from the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <returns>The review with the specific id</returns>
        [HttpGet("{id}", Name = "GET Review")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Review))]
        public IActionResult Get(string id) => Ok(new Review { Id = id });

        /// <summary>
        /// Creates a new review in the system.
        /// </summary>
        /// <param name="review">The review that should be created</param>
        /// <returns>The review created</returns>
        [HttpPost(Name = "POST Review")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Review))]
        public IActionResult Create([FromBody] Review review)
        {
            if (string.IsNullOrWhiteSpace(review.Id))
                review.Id = Guid.NewGuid().ToString();

            return Created("", review);
        }

        /// <summary>
        /// Updates an existing review in the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="review">The updated review</param>
        [HttpPut("{id}", Name = "PUT Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update(string id, [FromBody] Review review) { return Ok(); }

        /// <summary>
        /// Deletes an existing review from the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        [HttpDelete("{id}", Name = "DELETE Review")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(string id) { return NoContent(); }
    }
}
