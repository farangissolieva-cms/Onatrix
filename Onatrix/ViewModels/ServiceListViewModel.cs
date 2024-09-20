using Microsoft.AspNetCore.Mvc.Rendering;

namespace Onatrix.ViewModels;

public class ServiceListViewModel
{
	public int SelectedServiceId { get; set; }
	public List<SelectListItem> Services { get; set; } = [];	
}
