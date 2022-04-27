using System.ComponentModel.DataAnnotations;

namespace SuggestionApp.UI.Models;

public class CreateSuggestionModel
{
    [Required]
    [MaxLength(75)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [Display(Name = "Category")]
    public string CategoryId { get; set; } = string.Empty;
}
