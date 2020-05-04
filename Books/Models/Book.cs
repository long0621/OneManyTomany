using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Books.Models
{
    //OneToMany設定:一本書對應多個評論,每個評論只會指向一本書
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public virtual List<Review> Reviews { get; set; } //方便存取使用

        public virtual List<BookAuthor> BookAuthors { get; set; }
    }

    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Message { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }
    }

    //多對多:一Book對應多筆Author,一Author對應多筆Book, 需另外建立關聯table
    [Table("Authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<BookAuthor> BookAuthors { get; set; } 
    }


    [Table("BookAuthor")]
public class BookAuthor
{
    public int BookId { get; set; }
    //[JsonIgnore]
    public virtual Book Book { get; set; }
    public int AuthorId { get; set; }
    //[JsonIgnore]
    public virtual Author Author { get; set; }
}


}
