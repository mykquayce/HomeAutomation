using HomeAutomation.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeAutomation.Website.Pages;

public class IndexModel(ILogger<IndexModel> logger, IBackendClient client) : PageModel
{
	public void OnGet()
	{

	}

	public Task<IActionResult> OnPost()
	{
		client.AmpPowerToggleAsync();
		IActionResult page = base.Page();
		return Task.FromResult(page);
	}
}
