namespace HomeAutomation.Clients;

public interface IIdentityClient
{
	Task<(string, DateTimeOffset)> GetTokenAsync(CancellationToken cancellationToken = default);
}
