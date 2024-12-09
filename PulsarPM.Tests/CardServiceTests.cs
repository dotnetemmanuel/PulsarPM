using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using PulsarPM.Client.Services;
using PulsarPM.DAL;
using Shared;

namespace PulsarPM.Tests;

public class CardServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly ICardService _cardService;
    private readonly ILogger<CardService> _logger;

    public CardServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost/api/")
        };

        _logger = Mock.Of<ILogger<CardService>>();
        _cardService = new CardService(_httpClient, _logger);
    }

    [Fact]
    public async Task CreateCardAsync_ShouldReturnCreatedCardDTO()
    {
        // Arrange
        var newCard = new CardDTO
        {
            Id = 1,
            Name = "Test Card",
            Description = "Test Description",
            Status = "Backlog",
            Color = "Blue",
            ProjectId = 1,
            Order = 0
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
                    ""name"": ""Test Card"",
                    ""description"": ""Test Description"",
                    ""status"": ""Backlog"",
                    ""color"": ""Blue"",
                    ""projectId"": 1,
                    ""order"": 0
                   
                }")
            });

        // Act
        var result = await _cardService.CreateCardAsync(newCard);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(newCard.Id, result.Id);
        Assert.Equal(newCard.Name, result.Name);
        Assert.Equal(newCard.Description, result.Description);
        Assert.Equal(newCard.Status, result.Status);
        Assert.Equal(newCard.Color, result.Color);
        Assert.Equal(newCard.ProjectId, result.ProjectId);
        Assert.Equal(newCard.Order, result.Order);
    }

    [Fact]
    public async Task DeleteCardAsync_ShouldReturnTrue()
    {
        var cardToDelete = new CardDTO
        {
            Id = 1,
            Name = "Test Card",
            Description = "Test Description",
            Status = "Backlog",
            Color = "Blue",
            ProjectId = 1,
            Order = 0
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
        await _cardService.DeleteCardAsync(cardToDelete);

        //Assert
        Assert.True(true);
    }

    [Fact]
    public async Task GetCardsAsync_ShouldReturnAllCards()
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
                Content = new StringContent(@"[{
                    ""id"": 1,
                    ""name"": ""Test Card"",
                    ""description"": ""Test Description"",
                    ""status"": ""Backlog"",
                    ""color"": ""Blue"",
                    ""projectId"": 1,
                    ""order"": 0
                    
                },
                {
                    ""id"": 2,
                    ""name"": ""Test Card2"",
                    ""description"": ""Test Description2"",
                    ""status"": ""Backlog"",
                    ""color"": ""Green"",
                    ""projectId"": 1,
                    ""order"": 1
                }]")
            });

        //Act
        var result = await _cardService.GetCardsFromProjectAsync(projectId);

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
        //card 1
        Assert.Equal("Test Card", result[0].Name);
        Assert.Equal("Test Description", result[0].Description);
        Assert.Equal("Backlog", result[0].Status);
        Assert.Equal("Blue", result[0].Color);
        Assert.Equal(projectId, result[0].ProjectId);
        Assert.Equal(0, result[0].Order);

        //card 2
        Assert.Equal("Test Card2", result[1].Name);
        Assert.Equal("Test Description2", result[1].Description);
        Assert.Equal("Backlog", result[1].Status);
        Assert.Equal("Green", result[1].Color);
        Assert.Equal(projectId, result[1].ProjectId);
        Assert.Equal(1, result[1].Order);
    }
    
    [Fact]
    public async Task UpdateCardAsync_ShouldReturnUpdatedCardDTO()
    {
        // Arrange
        var updatedCard = new CardDTO
        {
            Id = 1,
            Name = "Test Card",
            Description = "Test Description",
            Status = "Backlog",
            Color = "Blue",
            ProjectId = 1,
            Order = 0
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
                    ""name"": ""Test Card"",
                    ""description"": ""Test Description"",
                    ""status"": ""Backlog"",
                    ""color"": ""Blue"",
                    ""projectId"": 1,
                    ""order"": 0
                   
                }")
            });

        // Act
        var result = await _cardService.UpdateCardAsync(updatedCard);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedCard.Id, result.Id);
        Assert.Equal(updatedCard.Name, result.Name);
        Assert.Equal(updatedCard.Description, result.Description);
        Assert.Equal(updatedCard.Status, result.Status);
        Assert.Equal(updatedCard.Color, result.Color);
        Assert.Equal(updatedCard.ProjectId, result.ProjectId);
        Assert.Equal(updatedCard.Order, result.Order);
    }
}