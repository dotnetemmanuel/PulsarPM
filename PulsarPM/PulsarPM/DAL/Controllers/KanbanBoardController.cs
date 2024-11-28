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
 
}
