using System.Collections;
using PulsarPM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace PulsarPM.DAL.Controllers;


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
  public async Task<ActionResult<ICollection<ProjectDTO>>> GetProjectsAsync()
  {
    var projects = await _context.Projects.Select(p => new ProjectDTO
      {
        Id = p.Id,
        Name = p.Name
      }).ToListAsync();

    return Ok(projects);
  }
}
