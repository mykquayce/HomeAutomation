using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.WebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class AmpController(Helpers.GlobalCache.IService service) : ControllerBase
{
	[HttpPut("mute/{state:alpha:required}")]
	public async Task<IActionResult> MuteAsync(string state)
	{
		if (!string.Equals("toggle", state, StringComparison.OrdinalIgnoreCase))
		{
			return base.NotFound(new { state, });
		}

		using var cts = new CancellationTokenSource(millisecondsDelay: 1_000);
		await service.SendMessageAsync("amp-mute-toggle", cts.Token);
		return base.Accepted(new { state, });
	}

	[HttpPut("power/{state:alpha:required}")]
	public async Task<IActionResult> PowerAsync(string state)
	{
		if (!string.Equals("toggle", state, StringComparison.OrdinalIgnoreCase))
		{
			return base.NotFound(new { state, });
		}

		using var cts = new CancellationTokenSource(millisecondsDelay: 1_000);
		await service.SendMessageAsync("amp-power-toggle", cts.Token);
		return base.Accepted(new { state, });
	}
}
