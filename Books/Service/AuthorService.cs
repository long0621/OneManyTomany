using Books.DBContext;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Service
{
    public class AuthorService
    {
        private readonly Context _context;//資料庫
        public AuthorService(Context context)
        {
            _context = context;
        }

        public async Task<object> CreateAuthor(Author newAuthor)
        {
            var check = await _context.Authors.Where(author => author.Name == newAuthor.Name)
                                              .FirstOrDefaultAsync();
            var message = "";
            if (check == null)
            {
                _context.Authors.Add(newAuthor);
                await _context.SaveChangesAsync();
                message = "新增成功!";
                return new { newAuthor, message };
            }
            else
            {
                message = "重複添加!";
                return new { newAuthor, message };
            }
        }
    }
}
