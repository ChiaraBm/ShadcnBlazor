namespace ShadcnBlazor;

internal class CancelDebouncer
{
    private readonly TimeSpan Delay;
    private CancellationTokenSource Cts = new();

    internal CancelDebouncer(TimeSpan delay)
    {
        Delay = delay;
    }

    internal void Start(Func<Task> action)
    {
        var cts = new CancellationTokenSource();
        Cts = cts;
        
        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(Delay, cts.Token);
                await action.Invoke();
            }
            catch (OperationCanceledException)
            {
                // Ignored
            }
        });
    }

    internal async Task CancelAsync()
    {
        await Cts.CancelAsync();
        Cts = new();
    }
}