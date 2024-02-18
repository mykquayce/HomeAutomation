using HomeAutomation.Clients;

namespace System.Net.Http;

public class IdentityHandler(IIdentityClient client) : DelegatingHandler
{
	protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (request.Headers.Contains("Authorization"))
		{
			return await base.SendAsync(request, cancellationToken);
		}

		(string token, _) = await client.GetTokenAsync(cancellationToken);

		request.Headers.Authorization = new Headers.AuthenticationHeaderValue("Bearer", token);

		return await base.SendAsync(request, cancellationToken);
	}
}
