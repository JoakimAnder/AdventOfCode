using AdventOfCode.Shared.Attributes;
using AdventOfCode.Solutions.Helpers;
using AdventOfCode.UI.BlazorApp.Components;
using AdventOfCode.UI.BlazorApp.Endpoints;
using AdventOfCode.UI.Shared.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddAdventOfCodeServices();
builder.Services.AddAttributedDependencies(typeof(Program).Assembly);
builder.Services.AddSolutions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AdventOfCode.UI.BlazorApp.Client._Imports).Assembly);

Puzzles.AddEndpoints(app);

app.Run();
