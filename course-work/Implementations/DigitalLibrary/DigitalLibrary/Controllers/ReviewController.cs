using DigitalLibrary.DTOs.ReviewDTOs;
using DigitalLibrary.Interfaces;
using DigitalLibrary.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public ReviewController(IReviewRepository reviewRepository, IBookRepository bookRepository, 
            IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get a paginated list of books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews = await _reviewRepository.GetAllAsync();

            var reviewDTO = reviews.Select(s => s.ToReviewDTO());

            return Ok(reviewDTO);
        }

        /// <summary>
        /// Get a review by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepository.GetByIdAsync(id);

            if(review == null)
            {
                return NotFound();
            }

            return Ok(review.ToReviewDTO());
        }

        /// <summary>
        /// Create a new review by userId and bookId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <param name="reviewDTO"></param>
        /// <returns></returns>
        [HttpPost("{userId:int},{bookId:int}")]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromRoute] int bookId, 
           [FromBody] CreateReviewRequestDTO reviewDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _bookRepository.BookExists(bookId))
            {
                return BadRequest("The specified book does not exist.");
            }

            if (!await _userRepository.UserExists(userId))
            {
                return BadRequest("The specified user does not exist.");
            }

            var reviewModel = reviewDTO.ToReviewFromCreateDTO(userId, bookId);
            await _reviewRepository.CreateAsync(reviewModel);
            return CreatedAtAction(nameof(GetById), new { id = reviewModel.ReviewId }, reviewModel.ToReviewDTO());
        }

        /// <summary>
        /// Update an existing review.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateReviewRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepository.UpdateAsync(id, updateDTO.ToReviewFromUpdateDTO());

            if(review == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(review.ToReviewDTO());
        }

        /// <summary>
        /// Delete an existing review.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewModel = await _reviewRepository.DeleteAsync(id);

            if(reviewModel == null)
            {
                return NotFound("Review does not exist");
            }

            return Ok(reviewModel);
        }
    }
}
