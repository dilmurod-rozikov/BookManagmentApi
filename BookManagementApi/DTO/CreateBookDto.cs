using System.ComponentModel.DataAnnotations;

namespace BookManagmentApi.DTO
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }
    }
}
