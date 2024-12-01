using AdventOfCode.Shared.Attributes;
using AdventOfCode.UI.BlazorApp.Client.Clients;
using AdventOfCode.UI.Shared.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRefitClient<IServerClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddAdventOfCodeServices();
builder.Services.AddAttributedDependencies(typeof(Program).Assembly);

await builder.Build().RunAsync();
