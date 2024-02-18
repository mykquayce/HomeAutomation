using Microsoft.Extensions.Options;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// cache
builder.Services.AddMemoryCache();

// delegating handlers
builder.Services
	.AddTransient<HttpMessageHandler>(_ => new HttpClientHandler { AllowAutoRedirect = false, })
	.AddTransient<CachingHandler>()
	.AddTransient<IdentityHandler>()
	.AddTransient<PhysicalAddressResolver>();

// configs
builder.Services
	.Configure<Helpers.GlobalCache.Config>(builder.Configuration.GetSection("amp"))
	.Configure<Helpers.GlobalCache.Models.MessagesDictionary>(builder.Configuration.GetSection("amp:messages"))
	.Configure<HomeAutomation.Models.IdentityConfig>(builder.Configuration.GetSection("IdentityServer"))
	.Configure<Helpers.Nanoleaf.Config>(builder.Configuration.GetSection("Nanoleaf"))
	.Configure<HomeAutomation.Models.NetworkDiscoveryConfig>(builder.Configuration.GetSection("NetworkDiscovery"));

// identity client
builder.Services
	.AddHttpClient<HomeAutomation.Clients.IIdentityClient, HomeAutomation.Clients.Concrete.IdentityClient>((provider, client) =>
	{
		var config = provider.GetRequiredService<IOptions<HomeAutomation.Models.IdentityConfig>>().Value;
		client.BaseAddress = config.BaseAddress;
	})
	.ConfigurePrimaryHttpMessageHandler<HttpMessageHandler>()
	.AddHttpMessageHandler<CachingHandler>();

// nanoleaf client
builder.Services
	.AddHttpClient<Helpers.Nanoleaf.IClient, Helpers.Nanoleaf.Concrete.Client>("NanoleafClient", (provider, client) =>
	{
		var config = provider.GetRequiredService<IOptions<Helpers.Nanoleaf.Config>>().Value;
		client.BaseAddress = config.BaseAddress;
	})
	.ConfigurePrimaryHttpMessageHandler<HttpMessageHandler>()
	.AddHttpMessageHandler<PhysicalAddressResolver>();

// network discovery client
builder.Services
	.AddHttpClient<HomeAutomation.Clients.INetworkDiscoveryClient, HomeAutomation.Clients.Concrete.NetworkDiscoveryClient>((provider, client) =>
	{
		var config = provider.GetRequiredService<IOptions<HomeAutomation.Models.NetworkDiscoveryConfig>>().Value;
		client.BaseAddress = config.BaseAddress;
	})
	.ConfigurePrimaryHttpMessageHandler<HttpMessageHandler>()
	.AddHttpMessageHandler<CachingHandler>()
	.AddHttpMessageHandler<IdentityHandler>();

// global cache client
builder.Services
	.AddTransient<Helpers.GlobalCache.IService, Helpers.GlobalCache.Concrete.Service>()
	.AddTransient<Helpers.GlobalCache.IClient, Helpers.GlobalCache.Concrete.Client>()
	.AddTransient(_ => new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
