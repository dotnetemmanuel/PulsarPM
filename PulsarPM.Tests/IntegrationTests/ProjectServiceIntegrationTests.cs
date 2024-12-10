namespace PulsarPM.Tests.IntegrationTests;

using System.Net.Http.Json;
using PulsarPM.Tests.Infrastructure;
using Shared;
using Xunit.Abstractions;
using Xunit.Sdk;
using XUnitPriority;


public class ProjectServiceIntegrationTests : IClassFixture<TestingWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;
  private readonly TestingWebApplicationFactory<Program> _factory;
  private readonly ITestOutputHelper _testOutputHelper;

  public ProjectServiceIntegrationTests(TestingWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
  {
    _factory = factory;
    _testOutputHelper = testOutputHelper;
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task Test01_GetProjects_ShouldReturnListOfProjects()
  {
    // Act
    var response = await _client.GetAsync("api/Project");
    response.EnsureSuccessStatusCode();

    var projects = await response.Content.ReadFromJsonAsync<List<ProjectDTO>>();

    // Assert
    Assert.NotNull(projects);
    _testOutputHelper.WriteLine($"Number of projects returned: {projects.Count}");
    foreach (var project in projects)
    {
      _testOutputHelper.WriteLine($"Project Name: {project.Name}");
    }

    Assert.Contains(projects, p => p.Name == "Test Project");
  }

  [Fact]
  public async Task Test02_GetProjectById_ShouldReturnSingleProject()
  {
    var response = await _client.GetAsync("api/Project/1");
    response.EnsureSuccessStatusCode();

    var project = await response.Content.ReadFromJsonAsync<ProjectDTO>();
    Assert.NotNull(project);
    Assert.Equal("Test Project", project.Name);
    _testOutputHelper.WriteLine($"Project Name: {project.Name}");
    _testOutputHelper.WriteLine($"Project Id: {project.Id}");
    _testOutputHelper.WriteLine($"Project archived: {project.IsArchived}");
  }

  [Fact]
  public async Task Test03_CreateProject_ShouldReturnCreatedProjectDTO()
  {
    // await _client.DeleteAsync("api/Project/1");
    var newProject = new ProjectDTO
    {
      Id = 2,
      Name = "Test Project 2",
      Cards = new List<CardDTO>(),
      IsArchived = false
    };

    var response = await _client.PostAsJsonAsync("api/Project", newProject);
    var statusCode = response.EnsureSuccessStatusCode();

    //Checks if newProject has been added to Db - should return 2 projects
    var responseProjects = await _client.GetAsync("api/Project");
    var projects = await responseProjects.Content.ReadFromJsonAsync<List<ProjectDTO>>();

    Assert.Equal("OK", statusCode.ReasonPhrase);
    Assert.Equal(2, projects.Count);
    _testOutputHelper.WriteLine($"{projects.Count}");

    Assert.Equal("Test Project 2", projects[1].Name);
    _testOutputHelper.WriteLine($"{projects[1].Name}");
  }

  // [Fact]
  // public async Task Test04_DeleteProject_ShouldReturnEmptyList()
  // {
  //   //Arrange
  //   var newProject = new ProjectDTO
  //   {
  //     Id = 2,
  //     Name = "Test Project",
  //     Cards = new List<CardDTO>(),
  //     IsArchived = false
  //   };
  //   await _client.PostAsJsonAsync("api/Project", newProject);
  //
  //   //Act
  //   var responseStartProjects = await _client.GetAsync("api/Project");
  //   var projects = await responseStartProjects.Content.ReadFromJsonAsync<List<ProjectDTO>>();
  //
  //   await _client.DeleteAsync("api/Project/2");
  //
  //   var responseFinalProjects = await _client.GetAsync("api/Project");
  //   var projectsLeft = await responseFinalProjects.Content.ReadFromJsonAsync<List<ProjectDTO>>();
  //
  //   //Assert
  //   Assert.Equal(2, projects.Count);
  //   Assert.Single(projectsLeft);
  // }
}
