namespace HomeAutomation.Models;

public record IdentityConfig(Uri BaseAddress, string ClientId, string ClientSecret, string Scope)
{
	public IdentityConfig() : this(default!, default!, default!, default!) { }
}
