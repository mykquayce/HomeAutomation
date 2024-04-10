using HomeAutomation.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeAutomation.Website.Pages;

public class IndexModel(ILogger<IndexModel> logger, IBackendClient client) : PageModel
{
	public void OnGet()
	{

	}

	public Task<IActionResult> OnPostAmpPowerToggleAsync()
	{
		client.AmpPowerToggleAsync();
		IActionResult page = base.Page();
		return Task.FromResult(page);
	}

	public Task<IActionResult> OnPostNanoleafPowerOffAsync()
	{
		client.NanoleafPowerSetAsync(false);
		IActionResult page = base.Page();
		return Task.FromResult(page);
	}

	public Task<IActionResult> OnPostNanoleafPowerOnAsync()
	{
		client.NanoleafPowerSetAsync(true);
		IActionResult page = base.Page();
		return Task.FromResult(page);
	}
}
