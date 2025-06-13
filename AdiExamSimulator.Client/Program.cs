using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using AdiExamSimulator.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using AdiExamSimulator.Client;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddScoped<AuthorizedHttpClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

// Add Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add AuthenticationStateProvider
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
