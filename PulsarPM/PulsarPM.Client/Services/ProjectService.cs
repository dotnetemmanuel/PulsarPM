namespace PulsarPM.Client.Services;

using System.Net.Http.Json;
using Shared;

public class ProjectService
{
  private readonly HttpClient _httpClient;

  public ProjectService(HttpClient httpClient)
  {
    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));;
  }

  public async Task<ICollection<ProjectDTO>> GetProjectsAsync()
  {
    var projects = await _httpClient.GetFromJsonAsync<List<ProjectDTO>>("api/Project");
    if (projects == null) throw new InvalidOperationException("No projects received from API");

    return projects;
  }
}
