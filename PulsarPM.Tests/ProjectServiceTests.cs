namespace PulsarPM.Tests;

using System.Net;
using Client.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Shared;

public class ProjectServiceTests
{
  private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
  private readonly HttpClient _httpClient;
  private readonly IProjectService _projectService;
  private readonly ILogger<ProjectService> _logger;

  public ProjectServiceTests()
  {
    _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

    _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
    {
      BaseAddress = new Uri("http://localhost/api/")
    };

    _logger = Mock.Of<ILogger<ProjectService>>();
    _projectService = new ProjectService(_httpClient, _logger);
  }

  [Fact]
  public async Task GetProjectsAsync_ShouldReturnListOfProjects()
  {
    //Arrange
    _mockHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>(
      "SendAsync",
      ItExpr.IsAny<HttpRequestMessage>(),
      ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"[{
                    ""id"": 1,
                    ""name"": ""Test Project"",
                    ""cards"": [],
                    ""isArchived"": false
                }]")
      });

    //Act
    var result = await _projectService.GetProjectsAsync();

    //Assert
    Assert.NotNull(result);
    Assert.Single(result);
    var project = result.First();
    Assert.Equal(1, project.Id);
    Assert.Equal("Test Project", project.Name);
    Assert.Empty(project.Cards);
    Assert.False(project.IsArchived);
  }

  [Fact]
  public async Task GetProjectByIdAsync_ShouldReturnProjectDTO()
  {
    //Arrange
    int projectId = 1;
    _mockHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>(
      "SendAsync",
      ItExpr.IsAny<HttpRequestMessage>(),
      ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
                    ""id"": 1,
                    ""name"": ""Test Project"",
                    ""cards"": [],
                    ""isArchived"": false
                }")
      });

    //Act
    var result = await _projectService.GetProjectByIdAsync(projectId);

    //Assert
    Assert.NotNull(result);
    Assert.Equal(1, result.Id);
    Assert.Equal("Test Project", result.Name);
    Assert.Empty(result.Cards);
    Assert.False(result.IsArchived);
  }

  [Fact]
  public async Task CreateProjectAsync_ShouldReturnCreatedProjectDTO()
  {
    //Arrange
    var newProject = new ProjectDTO
    {
      Name = "New Project"
    };

    _mockHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>(
      "SendAsync",
      ItExpr.IsAny<HttpRequestMessage>(),
      ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
                    ""id"": 1,
                    ""name"": ""Test Project"",
                    ""cards"": [],
                    ""isArchived"": false
                }")
      });

    //Act
    var result = await _projectService.CreateProjectAsync(newProject);

    //Assert
    Assert.NotNull(result);
    Assert.Equal(1, result.Id);
    Assert.Equal("Test Project", result.Name);
    Assert.Empty(result.Cards);
    Assert.False(result.IsArchived);
  }

  [Fact]
  public async Task DeleteProjectAsync() //TODO: add name suffix
  {
    // Arrange
    var projectToDelete = new ProjectDTO
    {
      Id = 1,
      Name = "Project To Delete"
    };

    _mockHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>(
      "SendAsync",
      ItExpr.IsAny<HttpRequestMessage>(),
      ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK
      });

    // Act
    await _projectService.DeleteProjectAsync(projectToDelete);

    //Assert
    Assert.True(true);
  }
}
