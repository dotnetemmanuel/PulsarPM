using System.Collections;
using PulsarPM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace PulsarPM.DAL.Controllers;

using Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
  [HttpGet("project/{projectId}")]
  public async Task<ActionResult<ICollection<CardDTO>>> GetCardsFromDbAsync(int projectId)
  {
    var cards = await _context.Cards.Where(c => c.ProjectId == projectId).Select(p => new CardDTO
    {
      Id = p.Id,
      Name = p.Name,
      Description = p.Description,
      Status = p.Status,
      Color = p.Color,
      ProjectId = p.ProjectId
    }).ToListAsync();

    return Ok(cards);
  }

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

  [HttpPut("project/{id}")]
  public async Task<ActionResult<Card>> UpdateCardToDbAsync(int id, [FromBody] CardDTO cardDto)
  {
    if (id != cardDto.Id)
    {
      return BadRequest("ID mismatch");
    }

    var cardToUpdate = await _context.Cards.FindAsync(cardDto.Id);
    if (cardToUpdate is null)
    {
      return NotFound("Card not found");
    }

    cardToUpdate.Name = cardDto.Name;
    cardToUpdate.Description = cardDto.Description;
    cardToUpdate.Status = cardDto.Status;
    cardToUpdate.Color = cardDto.Color;
    cardToUpdate.ProjectId = cardDto.ProjectId;

    await _context.SaveChangesAsync();
    return Ok(cardToUpdate);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<Card>> DeleteCardFromDbAsync(int id)
  {
    var cardToDelete = await _context.Cards.FindAsync(id);
    if (cardToDelete is null)
    {
      return NotFound("Card not found");
    }

    _context.Cards.Remove(cardToDelete);
    await _context.SaveChangesAsync();
    return Ok(cardToDelete);
  }
}
