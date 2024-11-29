namespace PulsarPM.Client.Services;

using System.Net.Http.Json;
using Shared;


public class CardService
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
    return response.ToList();
  }
  
}
