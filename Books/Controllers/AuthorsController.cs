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
    [Route("api/author")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorsController(AuthorService authorService)
        {
            this._authorService = authorService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<object>> Create(Author author)
        {
            var result = await _authorService.CreateAuthor(author);
            return result;
        }

    }
}