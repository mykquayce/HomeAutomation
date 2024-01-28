namespace HomeAutomation.Clients.Concrete;

public class BackendClient(HttpClient httpClient) : IBackendClient
{
	public async Task AmpPowerToggleAsync(CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsync("/amp/power/toggle", content: null, cancellationToken);
		response.EnsureSuccessStatusCode();
	}
}
