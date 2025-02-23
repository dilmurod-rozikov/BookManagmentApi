using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagmentApi.Models
{
    [Table("books")]
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }

        public int ViewCount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
