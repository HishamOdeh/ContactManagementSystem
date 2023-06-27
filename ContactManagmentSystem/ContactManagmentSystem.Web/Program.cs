using ContactManagementSystem.Web;
using ContactManagementSystem.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IHttpService, HttpService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
