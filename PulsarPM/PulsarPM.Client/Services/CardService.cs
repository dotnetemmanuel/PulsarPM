namespace PulsarPM.Client.Services;

using System.Net.Http.Json;
using MudBlazor;
using Shared;

public interface ICardService
{
  Task<CardDTO> CreateCardAsync(CardDTO cardDto);
  Task<List<CardDTO>> GetCardFromProjectAsync(int projectId);
  Task<CardDTO> UpdateCardAsync(CardDTO cardDto);
  Task DeleteCardAsync(CardDTO cardDto);
}

public class CardService : ICardService
{
  private readonly HttpClient _httpClient;
  private readonly ILogger<CardService> _logger;

  public CardService(HttpClient httpClient, ILogger<CardService> logger)
  {
    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task<CardDTO> CreateCardAsync(CardDTO cardDto)
  {
    var response = await _httpClient.PostAsJsonAsync("Card", cardDto);
    if (!response.IsSuccessStatusCode)
    {
      throw new Exception($"Failed to create card: {response.ReasonPhrase}");
    }
    return await response.Content.ReadFromJsonAsync<CardDTO>();
  }

  public async Task<List<CardDTO>> GetCardFromProjectAsync(int projectId)
  {
    var response = await _httpClient.GetFromJsonAsync<List<CardDTO>>($"Card/project/{projectId}");
    if (response is null)
    {
      throw new Exception($"No cards found for this project in the database");
    }
    return response.ToList();
  }

  public async Task<CardDTO> UpdateCardAsync(CardDTO cardDto)
  {
    var response = await _httpClient.PutAsJsonAsync($"Card/project/{cardDto.Id}", cardDto);
    if (!response.IsSuccessStatusCode)
    {
      throw new Exception($"Failed to update card: {response.ReasonPhrase}");
    }
    return await response.Content.ReadFromJsonAsync<CardDTO>();
  }

  public async Task DeleteCardAsync(CardDTO cardDto)
  {
    var response = await _httpClient.DeleteAsync($"Card/{cardDto.Id}");
    if (!response.IsSuccessStatusCode)
    {
      throw new Exception($"Failed to delete card: {response.ReasonPhrase}");
    }
  }

}
