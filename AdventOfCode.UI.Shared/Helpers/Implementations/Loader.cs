using AdventOfCode.Shared.Attributes;
using System.ComponentModel;

namespace AdventOfCode.UI.Shared.Helpers.Implementations;

[Transient(typeof(ILoader))]
public class Loader : ILoader, INotifyPropertyChanged
{
    private readonly List<IDisposable> _loaders = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                PropertyChanged?.Invoke(this, new(nameof(IsLoading)));
            }
        }
    }

    private void RefreshIsLoading()
    {
        IsLoading = _loaders.Count != 0;
    }

    private void StopLoading(IDisposable innerLoader)
    {
        _loaders.Remove(innerLoader);
        RefreshIsLoading();
    }

    public IDisposable StartLoading(Action? onFinished = null)
    {
        var loader = new InnerLoader(l =>
        {
            StopLoading(l);
            onFinished?.Invoke();
        });
        _loaders.Add(loader);
        RefreshIsLoading();
        return loader;
    }

    private struct InnerLoader(Action<IDisposable> stopLoading) : IDisposable
    {
        public readonly void Dispose() => stopLoading(this);
    }

}