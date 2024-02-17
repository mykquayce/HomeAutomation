using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HomeAutomation.WebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class NanoleafController(Helpers.Nanoleaf.IClient client) : ControllerBase
{
	[HttpPut("power/{value:regex(off|on)}")]
	public async Task<IActionResult> Power(string value)
	{
		using var cts = new CancellationTokenSource(millisecondsDelay: 100_000);
		var on = string.Equals("on", value, StringComparison.OrdinalIgnoreCase);
		var response = await client.SetOnAsync(on, cts.Token);

		if (response.IsSuccessStatusCode)
		{
			return Ok();
		}

		var o = new
		{
			response.StatusCode,
			body = await response.Content.ReadAsStringAsync(cts.Token),
		};

		return StatusCode((int)HttpStatusCode.InternalServerError, o);
	}
}
