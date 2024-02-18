using HomeAutomation.Models;
using System.Net.Http.Json;
using System.Web;

namespace HomeAutomation.Clients.Concrete;

public class NetworkDiscoveryClient(HttpClient httpClient) : INetworkDiscoveryClient
{
	public IAsyncEnumerable<DhcpLease> GetAllLeasesAsync(CancellationToken cancellationToken = default)
		=> httpClient.GetFromJsonAsAsyncEnumerable<DhcpLease>("api/router", cancellationToken);

	public Task ResetAsync(CancellationToken cancellationToken = default)
		=> httpClient.PutAsync("api/router/reset", content: null, cancellationToken);

	public Task<DhcpLease> ResolveAsync(object key, CancellationToken cancellationToken = default)
	{
		var requestUri = new Uri("api/router/" + HttpUtility.UrlPathEncode(key.ToString()), UriKind.Relative);
		return httpClient.GetFromJsonAsync<DhcpLease>(requestUri, cancellationToken);
	}
}
