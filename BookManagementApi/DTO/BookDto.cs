using System.ComponentModel.DataAnnotations;

namespace BookManagmentApi.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }

        public int ViewCount { get; set; }
    }
}
