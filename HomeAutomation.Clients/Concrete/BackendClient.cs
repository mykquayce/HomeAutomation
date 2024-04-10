namespace HomeAutomation.Clients.Concrete;

public class BackendClient(HttpClient httpClient) : IBackendClient
{
	public async Task AmpPowerToggleAsync(CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsync("/amp/power/toggle", content: null, cancellationToken);
		response.EnsureSuccessStatusCode();
	}

	public async Task NanoleafPowerSetAsync(bool on, CancellationToken cancellationToken = default)
	{
		var value = on ? "on" : "off";
		var response = await httpClient.PutAsync("/nanoleaf/power/" + value, content: null, cancellationToken);
		response.EnsureSuccessStatusCode();
	}
}
