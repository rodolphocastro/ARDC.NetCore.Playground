using ARDC.NetCore.Playground.Domain;
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
        private readonly IUnitOfWork _unitOfWork;
        private IReviewRepository ReviewRepository => _unitOfWork.ReviewRepository;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Get all the reviews in the system,
        /// </summary>
        /// <returns>A list of reviews</returns>
        [HttpGet(Name = "GET Reviews")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Review>))]
        public IActionResult Get() => Ok(ReviewRepository.Get());

        /// <summary>
        /// Get a specific review from the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <returns>The review with the specific id</returns>
        [HttpGet("{id}", Name = "GET Review")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Review))]
        public IActionResult Get(string id) => Ok(ReviewRepository.Get(id));

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

            var createdReview = ReviewRepository.Create(review);

            _unitOfWork.SaveChanges();

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
                ReviewRepository.Update(id, review);
            }
            catch (Exception)   //TODO: Validar qual o erro ocorrido
            {
                return BadRequest();    //TODO: Com base na exception, decidir qual resposta retornar
            }

            _unitOfWork.SaveChanges();

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
                ReviewRepository.Delete(id);
            }
            catch (Exception)   //TODO: Validar qual o erro ocorrido
            {
                return BadRequest();    //TODO: Com base na exception, decidir qual resposta retornar
            }

            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
