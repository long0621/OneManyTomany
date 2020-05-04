using Books.DBContext;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Service
{
    public class BookService
    {
        private readonly Context _context;//資料庫

        public BookService(Context context)
        {
            _context = context;
        }

        //create
        public async Task<object> CreateBook(Book book)
        {
            var check = await _context.Books.Where(bookItem => bookItem.Title == book.Title)
                                   .FirstOrDefaultAsync();
            var message = "";
            if (check == null) 
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                message = "新增成功!";
                return new { book, message };
            }
            else
            {
                message = "重複添加!";
                return new { book, message };
            }
            
        }
        //Read all
        public async Task<IEnumerable<Book>> ReadAll()
        {
            //var search = await _context.Books.ToListAsync();
            var search = await _context.Books.Include(book => book.Reviews).ToListAsync();
            
            return search; 
        }
        //Read by id
        public async Task<object> ReadBookById(int Id)
        {
            var checkBook =  await _context.Books.Where(book =>book.Id== Id)
                                                 .Include(book => book.Reviews)
                                                 .Include(book => book.BookAuthors).ThenInclude(bookAuthor => bookAuthor.Author)
                                                 .FirstOrDefaultAsync();
            var message = "";

            if (checkBook == null) message = "查無結果!";
            else
            {
                message = "查詢成功!";
            }
            
            return new
            {
                book = checkBook,
                message
            };
        }
        //UpdateBook
        public async Task<object>UpdateBook(Book updateBook)
        {
            var books = _context.Books;//可加入.AsNoTracking()強制寫入物件
            var checkedBook = await _context.Books.Where(bookItem => bookItem.Id == updateBook.Id)
                                                  .Include(bookItem => bookItem.Reviews)
                                                  .Include(bookItem=> bookItem.BookAuthors)
                                                  .FirstOrDefaultAsync();
            var message = "";

            if (checkedBook == null) message = "查無結果!";
            else
            {
                checkedBook.Title = updateBook.Title;  //1.考慮到有些欄位不能任意修改(如更新時間等),故不直接傳入物件
                checkedBook.Pages = updateBook.Pages;
                checkedBook.Reviews = updateBook.Reviews;//2.可直接修改導覽屬性
                checkedBook.BookAuthors = updateBook.BookAuthors;

                //測試一對多:使用更新來進行綁定,之後調用ReadBookById搭配NewtonsoftJson套件做顯示
                //var test = new BookAuthor() { BookId =1,AuthorId=1};
                //var test1 = new BookAuthor() { BookId = 2, AuthorId = 2 };
                //checkedBook.BookAuthors.Add(test); 
                //checkedBook.BookAuthors.Add(test1);

                await _context.SaveChangesAsync();
                message = "更新成功!";
            }

            return new
            {
                checkedBook,
                message
            };
        }

        //DeleteBook
        public async Task<object> DeleteBook(int Id)
        {
            var books = _context.Books;
            var checkBook = await (from bookItem in books
                             where bookItem.Id == Id
                             select bookItem)
                        .FirstOrDefaultAsync();
            var message = "";

            if (checkBook == null) message = "查無結果!";
            else
            {
                books.Remove(checkBook);
                _context.SaveChanges();
                message = "刪除成功!";
            }
            return new
            {
                book = checkBook,
                message
            };
        }

    }
}
