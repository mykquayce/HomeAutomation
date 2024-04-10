var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddHttpClient<HomeAutomation.Clients.IBackendClient, HomeAutomation.Clients.Concrete.BackendClient>(client =>
	{
		var uriString = builder.Configuration["Backend"] ?? throw new KeyNotFoundException("backend");
		client.BaseAddress = new Uri(uriString);
	})
	.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false, });

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
