namespace HomeAutomation.Models;

public record NetworkDiscoveryConfig(Uri BaseAddress)
{
	public NetworkDiscoveryConfig() : this(BaseAddress: default!) { }
}
