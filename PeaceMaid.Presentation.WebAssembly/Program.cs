using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PeaceMaid.Application.Services;
using PeaceMaid.Application.Services.ServiceProviders;
using PeaceMaid.Application.Services.Svcs;
using PeaceMaid.Presentation.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7206") });
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISvcsService, SvcsServices>();
builder.Services.AddScoped<ISvcProviderService, SvcProviderService>();
builder.Services.AddBlazorBootstrap(); // Bootstrap Blazor



await builder.Build().RunAsync();
