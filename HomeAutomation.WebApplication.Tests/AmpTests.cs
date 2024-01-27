using Microsoft.AspNetCore.Mvc.Testing;

namespace HomeAutomation.WebApplication.Tests;

public class AmpTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
	private static readonly WebApplicationFactoryClientOptions _options = new() { AllowAutoRedirect = false, };
	private readonly HttpClient _httpClient = factory.CreateClient(_options);

	[Theory]
	[InlineData("/amp/power/toggle")]
	[InlineData("/amp/mute/toggle")]
	public async Task PowerTests(string requestUri)
	{
		// Act
		var response = await _httpClient.PutAsync(requestUri, content: null);

		// Assert
		Assert.True(
			response.IsSuccessStatusCode,
			userMessage: response.StatusCode + " " + await response.Content.ReadAsStringAsync());
	}
}
