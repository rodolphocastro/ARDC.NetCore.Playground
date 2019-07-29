using ARDC.NetCore.Playground.API.ViewModels.ReviewViewModels;
using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using AutoMapper;
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
        private readonly IMapper _mapper;

        private IReviewRepository ReviewRepository => _unitOfWork.ReviewRepository;

        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all the reviews in the system,
        /// </summary>
        /// <returns>A list of reviews</returns>
        [HttpGet(Name = "GET Reviews")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReviewList>))]
        public IActionResult Get()
        {
            var models = ReviewRepository.Get();
            var viewModels = _mapper.Map<IList<ReviewList>>(models);
            return Ok(viewModels);
        }

        /// <summary>
        /// Get a specific review from the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <returns>The review with the specific id</returns>
        [HttpGet("{id}", Name = "GET Review")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewView))]
        public IActionResult Get(string id)
        {
            var model = ReviewRepository.Get(id);
            var viewModel = _mapper.Map<ReviewView>(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates a new review in the system.
        /// </summary>
        /// <param name="review">The review that should be created</param>
        /// <returns>The review created</returns>
        [HttpPost(Name = "POST Review")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReviewView))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ReviewCreate review)
        {
            var model = _mapper.Map<Review>(review);

            var createdReview = ReviewRepository.Create(model);

            _unitOfWork.SaveChanges();

            return Created("", _mapper.Map<ReviewView>(createdReview));
        }

        /// <summary>
        /// Updates an existing review in the system.
        /// </summary>
        /// <param name="id">The review's id</param>
        /// <param name="review">The updated review</param>
        [HttpPut("{id}", Name = "PUT Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update(string id, [FromBody] ReviewEdit review)
        {
            var model = ReviewRepository.Get(id);
            _mapper.Map(review, model);

            try
            {
                ReviewRepository.Update(id, model);
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
