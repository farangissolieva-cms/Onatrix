using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Onatrix.ViewModels;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix.Controllers;

public class FormSurfaceController : SurfaceController
{
	private readonly IUmbracoHelperAccessor _umbracoHelperAccessor;

	public FormSurfaceController(IUmbracoHelperAccessor umbracoHelperAccessor, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
	{
		_umbracoHelperAccessor = umbracoHelperAccessor;
	}

	[HttpPost]
	public IActionResult RequestForm(RequestFormViewModel form)
	{
		if (!ModelState.IsValid)
			return CurrentUmbracoPage();

		return RedirectToCurrentUmbracoPage();
	}

}
