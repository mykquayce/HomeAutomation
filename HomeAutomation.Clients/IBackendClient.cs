namespace HomeAutomation.Clients;

public interface IBackendClient
{
	Task AmpPowerToggleAsync(CancellationToken cancellationToken = default);
}
