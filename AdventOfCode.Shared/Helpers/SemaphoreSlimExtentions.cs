namespace AdventOfCode.Shared.Helpers;
public static class SemaphoreSlimExtentions
{
    public static async ValueTask<IDisposable> WaitDisposable(this SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync();
        return new DisposableRelease(semaphore);
    }

    private struct DisposableRelease(SemaphoreSlim semaphore) : IDisposable
    {
        public readonly void Dispose() => semaphore.Release();
    }
}