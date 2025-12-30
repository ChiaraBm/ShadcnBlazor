namespace ShadcnBlazor.Extras;

// From https://github.com/ChiaraBm/MoonCore/blob/main/Library/MoonCore/Helpers/Debouncer.cs

/// <summary>
/// Provides a mechanism to debounce actions, ensuring that a specified action
/// is invoked only after a defined interval of inactivity.
/// </summary>
internal class Debouncer
{
    private readonly TimeSpan Delay;
    private CancellationTokenSource Cts = new();

    /// <summary>
    /// Initializes a debouncer with <see cref="delayMs"/> amount of delay in miliseconds
    /// </summary>
    /// <param name="delayMs">Delay in milliseconds</param>
    internal Debouncer(int delayMs = 300)
    {
        Delay = TimeSpan.FromMilliseconds(delayMs);
    }
    
    /// <summary>
    /// Initializes the debouncer with the specified delay
    /// </summary>
    /// <param name="delay">Delay for the debouncer</param>
    internal Debouncer(TimeSpan delay)
    {
        Delay = delay;
    }

    /// <summary>
    /// Invokes the provided callback after the constructor defined delay of inactivity
    /// </summary>
    /// <param name="action">Callback to debounce</param>
    internal void Debounce(Func<Task> action)
    {
        // Cancel any existing scheduled task
        Cts.Cancel();
        Cts.Dispose();

        // Create a new token
        Cts = new CancellationTokenSource();
        var token = Cts.Token;

        // Schedule the task
        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(Delay, token);
                if (!token.IsCancellationRequested)
                {
                    await action();
                }
            }
            catch (TaskCanceledException)
            {
                // Expected if a new debounce event happens before the delay
            }
        }, token);
    }

    /// <summary>
    /// Invokes the provided callback after the constructor defined delay of inactivity
    /// </summary>
    /// <param name="action">Callback to debounce</param>
    internal void Debounce(Action action)
    {
        Debounce(() =>
        {
            action();
            return Task.CompletedTask;
        });
    }
}