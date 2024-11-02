namespace AdventOfCode.UI.Shared.Helpers;
public interface ILoader
{
    bool IsLoading { get; }
    IDisposable StartLoading();
}
