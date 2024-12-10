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

  //run all tests Ctrl+U Ctrl+L
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
                },
                {
                    ""id"": 2,
                    ""name"": ""Test Project2"",
                    ""cards"": [],
                    ""isArchived"": false
                }]")
      });

    //Act
    var result = await _projectService.GetProjectsAsync();

    //Assert
    Assert.NotNull(result);

    //project 1
    Assert.Equal(1, result[0].Id);
    Assert.Equal("Test Project", result[0].Name);
    Assert.Empty(result[0].Cards);
    Assert.False(result[0].IsArchived);

    //project 2
    Assert.Equal(2, result[1].Id);
    Assert.Equal("Test Project2", result[1].Name);
    Assert.Empty(result[1].Cards);
    Assert.False(result[1].IsArchived);
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
    Assert.Equal(projectId, result.Id);
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
      Id = 1,
      Name = "Test Project",
      Cards = new List<CardDTO>(),
      IsArchived = false
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
    Assert.Equal(newProject.Id, result.Id);
    Assert.Equal(newProject.Name, result.Name);
    Assert.Equal(newProject.Cards, result.Cards);
    Assert.Equal(newProject.IsArchived, result.IsArchived);
  }

  [Fact]
  public async Task DeleteProjectAsync_ShouldReturnTrue()//TODO: add name suffix
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
