using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace Books.DBContext //不能與下面DbContext同名
{
    
    public class Context : DbContext
    {
        //在輸出印出SQL語法
        public static readonly LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
                new DebugLoggerProvider()
            });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }



        public Context(DbContextOptions<Context> options) : base(options) { }

        //一對多
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        //多對多
        public DbSet<Author> Authors { get; set; }

        //fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //一對多設定
            modelBuilder.Entity<Review>() //Review只對應一筆Book,Book能對應多筆Review,宣告ForeignKey
                .HasOne(author => author.Book)
                .WithMany(book=>book.Reviews)
                .HasForeignKey(author => author.BookId);

            //多對多:雙向設定
            modelBuilder.Entity<BookAuthor>()
                .HasKey(bookAuthor => new { bookAuthor.BookId, bookAuthor.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Book)
                .WithMany(author => author.BookAuthors)
                .HasForeignKey(bookAuthor => bookAuthor.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Author)
                .WithMany(book => book.BookAuthors)
                .HasForeignKey(bookAuthor => bookAuthor.AuthorId);
        }
    }
}
