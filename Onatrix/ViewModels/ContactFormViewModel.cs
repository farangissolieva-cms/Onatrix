using System.ComponentModel.DataAnnotations;

namespace Onatrix.ViewModels;

public class ContactFormViewModel
{
    [Display(Name = "Email address", Prompt = "E-mail address", Order = 1)]
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;
}
