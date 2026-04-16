using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المشروع مطلوب")]
        [Display(Name = "اسم المشروع")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "وصف المشروع مطلوب")]
        [Display(Name = "وصف المشروع")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "صورة المشروع")]
        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
