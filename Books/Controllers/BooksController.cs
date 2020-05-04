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
    [Route("api/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<object>> Create(Book book)
        {
            var result = await _bookService.CreateBook(book);
            return result;
        }

        [HttpGet("readAll")]
        public async Task<IEnumerable<Book>> ReadAll()
        {
            var result = await _bookService.ReadAll();
            return result;
        }

        [HttpPost("readById")]
        public async Task<ActionResult<object>> ReadById(IdObj idObj)
        {
            var result = await _bookService.ReadBookById(idObj.Id);
            return result;
        }

        [HttpPost("update")]
        public async Task<ActionResult<object>> Update(Book book)
        {
            var result = await _bookService.UpdateBook(book);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<object>> Delete(IdObj idOb)
        {
            var result = await _bookService.DeleteBook(idOb.Id);
            return result;
        }
    }
}