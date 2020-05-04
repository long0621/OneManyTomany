using Books.DBContext;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Service
{
    public class ReviewService
    {
        private readonly Context _context;//資料庫

        public ReviewService(Context context)
        {
            _context = context;
        }


        //create
        public async Task<object> CreateReview(Review review)
        {
            var reviews = _context.Reviews;
            var bookUpdate = await _context.Books.FindAsync(review.BookId);
            var message = "";

            if (bookUpdate == null)
            {
                message = "找不到 BookId!";
                return new { message};
            }
            reviews.Add(review);

            await _context.SaveChangesAsync();

            message = "新增成功!";
            return new { message };
        }

        public async Task<object> DeleteReview(int Id)
        {
            var bookDelete = await _context.Books.FindAsync(Id);
            var message = "";
            if (bookDelete == null)
            {
                message = "找不到 ReviewId!";
                return new { message };
            }
            
            await _context.SaveChangesAsync();
            message = "刪除成功!";
            return new { bookDelete,message };
        }
    }
}
