using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ReviewService _reviewService;
        public ReviewsController(BookService bookService, ReviewService reviewService)
        {
            this._bookService = bookService;
            this._reviewService = reviewService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<object>> Create(Review review)
        {
            var result = await _reviewService.CreateReview(review);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<object>> Delete(IdObj idOb)
        {
            var result = await _reviewService.DeleteReview(idOb.Id);
            return result;
        }

    }
}