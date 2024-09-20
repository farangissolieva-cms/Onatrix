using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Onatrix.ViewModels;

public class RequestFormViewModel
{
    [Display(Name = "Name", Prompt = "Name", Order = 0)]
    [Required(ErrorMessage = "Full name is required")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = null!;


    [Display(Name = "Email address", Prompt = "Email address", Order = 1)]
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    [Display(Name = "Phone", Prompt = "Phone", Order = 3)]
    [Required(ErrorMessage = "Phone is required")]
    [DataType(DataType.MultilineText)]
    public string Phone { get; set; } = null!;


    [Display(Name = "Services", Prompt = "Choose the service you are interested in", Order = 2)]
    public int? SelectedServiceId { get; set; }
    public string? ServiceName { get; set; }

	public List<SelectListItem> Services { get; set; } = [];

    public string ButtonText { get; set; } = null!;
    public string ButtonColorClassName { get; set; } = null!;
}
