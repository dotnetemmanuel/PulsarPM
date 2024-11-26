namespace PulsarPM.Client.Services;

public class ProjectStateService
{
  private TaskCompletionSource<bool> _projectAddedTcs = new TaskCompletionSource<bool>();
  public event Func<Task> OnProjectAdded;

  public async Task NotifyProjectAddedAsync()
  {
    var handler = OnProjectAdded;
    if (handler != null)
    {
      await handler.Invoke();
    }
    _projectAddedTcs.TrySetResult(true);
  }
}
