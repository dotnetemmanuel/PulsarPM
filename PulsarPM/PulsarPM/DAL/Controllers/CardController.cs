using Microsoft.AspNetCore.Mvc;

namespace PulsarPM.DAL.Controllers;

using Data;
using Shared;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class CardController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public CardController(ApplicationDbContext context)
  {
    _context = context;
  }

  //GET
  [HttpGet("{id}")]
  public async Task<ActionResult<Card>> GetSingleCardFromDbAsync(int id)
  {
    var card = await _context.Cards.FindAsync(id);
    return Ok(card);
  }

  // POST
  [HttpPost]
  public async Task<ActionResult<Card>> CreateCardToDbAsync([FromBody] CardDTO cardDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var card = new Card
    {
      Name = cardDto.Name,
      Description = cardDto.Description,
      Status = cardDto.Status,
      Color = cardDto.Color,
      ProjectId = cardDto.ProjectId
    };
    try
    {
      await _context.Cards.AddAsync(card);
      await _context.SaveChangesAsync();

      var createdCardDto = new CardDTO
      {
        Id = card.Id,
        Name = card.Name,
        Description = card.Description,
        Status = card.Status,
        Color = card.Color,
        ProjectId = card.ProjectId

      };

      return Ok(createdCardDto);
    }
    catch (Exception ex)
    {
      return BadRequest($"Failed to create card: {ex.Message}");
    }
  }


}
