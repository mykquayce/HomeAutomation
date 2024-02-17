namespace HomeAutomation.Clients;

public interface IBackendClient
{
	Task AmpPowerToggleAsync(CancellationToken cancellationToken = default);
	Task NanoleafPowerSetAsync(bool on, CancellationToken cancellationToken = default);
}
