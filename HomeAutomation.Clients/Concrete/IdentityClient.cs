using HomeAutomation.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace HomeAutomation.Clients.Concrete;

public class IdentityClient(HttpClient httpClient, IOptions<IdentityConfig> options) : IIdentityClient
{
	private readonly IdentityConfig _config = options.Value;

	public async Task<(string, DateTimeOffset)> GetTokenAsync(CancellationToken cancellationToken = default)
	{
		var now = DateTimeOffset.UtcNow;

		var disco = await httpClient.GetDiscoveryDocumentAsync(cancellationToken: cancellationToken);
		if (disco.IsError) { throw new Exception(disco.Error); }

		using var tokenRequest = new ClientCredentialsTokenRequest
		{
			Address = disco.TokenEndpoint,
			ClientId = _config.ClientId,
			ClientSecret = _config.ClientSecret,
			Scope = _config.Scope,
		};
		var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(tokenRequest, cancellationToken);
		if (tokenResponse.IsError) { throw new Exception(tokenResponse.Error); }

		return (tokenResponse.AccessToken!, now.AddSeconds(tokenResponse.ExpiresIn));
	}
}
