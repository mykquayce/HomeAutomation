namespace HomeAutomation.Clients;

public interface INetworkDiscoveryClient
{
	IAsyncEnumerable<Models.DhcpLease> GetAllLeasesAsync(CancellationToken cancellationToken = default);
	Task ResetAsync(CancellationToken cancellationToken = default);
	Task<Models.DhcpLease> ResolveAsync(object key, CancellationToken cancellationToken = default);
}
