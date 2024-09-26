using System.ComponentModel.DataAnnotations;

namespace Onatrix.ViewModels;

public class QuestionFormViewModel
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

	[Display(Name = "Question", Prompt = "Question", Order = 2)]
	[Required(ErrorMessage = "Question is required")]
	[DataType(DataType.MultilineText)]
	public string Question { get; set; } = null!;

	public string? ButtonText { get; set; }
	public string? ButtonColorClassName { get; set; }
}
