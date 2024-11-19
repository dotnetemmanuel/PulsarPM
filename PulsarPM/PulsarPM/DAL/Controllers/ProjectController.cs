using Microsoft.AspNetCore.Mvc;

namespace PulsarPM.DAL.Controllers;

using System.Collections;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public ProjectController(ApplicationDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  // GET
  public async Task<ActionResult<ICollection<Project>>> GetProjectsAsync()
  {
    var projects = await _context.Projects.Select(p => new ProjectDTO
      {
        Id = p.Id,
        Name = p.Name
      }).ToListAsync();

    return Ok(projects);
  }
}
