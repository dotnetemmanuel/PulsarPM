
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared;

namespace PulsarPM.Client.Services;
public class ProjectService
{
  private readonly HttpClient _httpClient;
  private readonly ILogger<ProjectService> _logger;


  public ProjectService(HttpClient httpClient, ILogger<ProjectService> logger)
  {
    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task<List<ProjectDTO>> GetProjectsAsync()
  {
    try
    {
      _logger.LogInformation("Fetching projects from API");
      var projects = await _httpClient.GetFromJsonAsync<List<ProjectDTO>>("api/Project");
      if (projects == null) throw new InvalidOperationException("No projects received from API");

      _logger.LogInformation("Successfully fetched projects from API");
      return projects;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error fetching projects from API");
      throw;
    }
  }

  public async Task CreateProjectAsync(ProjectDTO projectDto)
  {
    var response = await _httpClient.PostAsJsonAsync<ProjectDTO>("api/Project", projectDto);
    if (!response.IsSuccessStatusCode)
    {
      throw new Exception($"Failed to create project: {response.ReasonPhrase}");
    }
  }

  public async Task DeleteProjectAsync(ProjectDTO projectDto)
  {
    var response = await _httpClient.DeleteAsync($"api/Project/{projectDto.Id}" );
    if (!response.IsSuccessStatusCode)
    {
      throw new Exception($"Failed to delete project: {response.ReasonPhrase}");
    }

  }


}
