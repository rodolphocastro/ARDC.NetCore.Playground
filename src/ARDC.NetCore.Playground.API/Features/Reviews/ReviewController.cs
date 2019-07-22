using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ARDC.NetCore.Playground.API.Features.Reviews
{
    [Route("reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        /// <summary>
        /// Get all the reviews in the system,
        /// </summary>
        /// <returns>A list of reviews</returns>
        [HttpGet(Name = "GET Reviews")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Review>))]
        public IActionResult Get() => Ok(_reviewRepository.Get());

        /// <summary>
        /// Get a specific review from the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <returns>The review with the specific id</returns>
        [HttpGet("{id}", Name = "GET Review")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Review))]
        public IActionResult Get(string id) => Ok(_reviewRepository.Get(id));

        /// <summary>
        /// Creates a new review in the system.
        /// </summary>
        /// <param name="review">The review that should be created</param>
        /// <returns>The review created</returns>
        [HttpPost(Name = "POST Review")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Review))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Review review)
        {
            if (!string.IsNullOrWhiteSpace(review.Id))
                return BadRequest();

            var createdReview = _reviewRepository.Create(review);
            return Created("", createdReview);
        }

        /// <summary>
        /// Updates an existing review in the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="review">The updated review</param>
        [HttpPut("{id}", Name = "PUT Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update(string id, [FromBody] Review review)
        {
            try
            {
                _reviewRepository.Update(id, review);
            }
            catch (Exception)   //TODO: Validar qual o erro ocorrido
            {
                return BadRequest();    //TODO: Com base na exception, decidir qual resposta retornar
            }

            return Ok();
        }

        /// <summary>
        /// Deletes an existing review from the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        [HttpDelete("{id}", Name = "DELETE Review")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(string id)
        {
            try
            {
                _reviewRepository.Delete(id);
            }
            catch (Exception)   //TODO: Validar qual o erro ocorrido
            {
                return BadRequest();    //TODO: Com base na exception, decidir qual resposta retornar
            }
            return NoContent();
        }
    }
}
