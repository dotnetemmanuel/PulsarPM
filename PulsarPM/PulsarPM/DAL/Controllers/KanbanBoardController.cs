using Microsoft.AspNetCore.Mvc;

namespace PulsarPM.DAL.Controllers;

using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

[ApiController]
[Route("api/[controller]")]
public class KanbanBoardController : Controller
{
  private readonly ApplicationDbContext _context;

  public KanbanBoardController(ApplicationDbContext context)
  {
    _context = context;
  }
  // // GET
  // [HttpGet]
  // public async Task<ActionResult<ICollection<KanbanBoardDTO>>> GetKanbanBoardsFromDbAsync()
  // {
  //   var kanbanBoards = await _context.KanbanBoards.Select(k => new KanbanBoardDTO
  //   {
  //     Id = k.Id,
  //     Name = k.Name,
  //     ProjectId = k.ProjectId
  //   }).ToListAsync();
  //
  //   return Ok(kanbanBoards);
  // }
  //
  // [HttpGet("{id}")]
  // public async Task<ActionResult<KanbanBoard>> GetKanbanBoardFromProjectFromDbAsync(int id)
  // {
  //   var project = await _context.Projects.FindAsync(id);
  //
  // }
}
