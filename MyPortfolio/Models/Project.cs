using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Url]
        public string GitHubLink { get; set; }
    }
}
