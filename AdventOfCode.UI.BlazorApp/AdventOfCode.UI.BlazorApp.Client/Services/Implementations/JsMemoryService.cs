using AdventOfCode.Shared.Attributes;
using AdventOfCode.UI.Shared.Services;
using Microsoft.JSInterop;
using System.Text.Json;

namespace AdventOfCode.UI.BlazorApp.Client.Services.Implementations;

[Singleton(typeof(IMemoryService))]
public class JsMemoryService(IJSRuntime jsRuntime) : IMemoryService, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/memory.js").AsTask());

    public ValueTask ClearMemory() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    public async ValueTask<MemoryUsageResult> GetMemoryUsage()
    {
        var module = await moduleTask.Value;
        var jsResult = await module.InvokeAsync<string>("getMemoryUsage");
        var result = JsonSerializer.Deserialize<MemoryUsageResult>(jsResult);
        return result;
    }
}
