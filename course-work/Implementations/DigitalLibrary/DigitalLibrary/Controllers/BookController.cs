using DigitalLibrary.Data;
using DigitalLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DigitalLibrary.DTOs.BookDTOs;
using DigitalLibrary.Mappers;
using DigitalLibrary.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace DigitalLibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;

        public BookController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _context = context;
        }

        /// <summary>
        /// Get a paginated list of books.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var books = await _bookRepository.GetAllAsync(query);

            var bookDTO = books.Select(s => s.ToBookDTO());

            return Ok(books);
        }

        /// <summary>
        /// Get a book by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepository.GetByIdAsync(id);

            if(book == null)
            {
                return NotFound();
            }

            return Ok(book.ToBookDTO());
        }

        /// <summary>
        /// Get a book by Title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string title)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepository.GetByTitleAsync(title);

            if(book == null)
            {
                return NotFound();
            }

            return Ok(book.ToBookDTO());
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDTO bookDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookModel = bookDTO.ToBookFromCreateDTO();
            await _bookRepository.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetById), new {id = bookModel.BookId}, bookModel.ToBookDTO());
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDTO bookUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookModel = await _bookRepository.UpdateAsync(id, bookUpdateDTO);

            if(bookModel == null)
            {
                return NotFound();
            }

            return Ok(bookModel.ToBookDTO());
        }

        /// <summary>
        /// Delete an existing book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookModel = await _bookRepository.DeleteAsync(id);

            if(bookModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
