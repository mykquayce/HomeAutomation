using Microsoft.Extensions.Caching.Memory;

namespace System.Net.Http;

public class CachingHandler(IMemoryCache cache) : DelegatingHandler
{
	protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var (code, body) = await cache.GetOrCreateAsync(request.RequestUri!.LocalPath, factory);

		return new(code) { Content = new ByteArrayContent(body), };

		async Task<(HttpStatusCode, byte[])> factory(ICacheEntry entry)
		{
			entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(.9);
			var response = await base.SendAsync(request, cancellationToken);
			var code = response.StatusCode;
			var body = await response.Content.ReadAsByteArrayAsync(cancellationToken);
			return (code, body);
		}
	}
}
