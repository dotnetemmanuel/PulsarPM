using System.Collections;
using PulsarPM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace PulsarPM.DAL.Controllers;

using Client.Services;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public ProjectController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET
  [HttpGet]
  public async Task<ActionResult<ICollection<ProjectDTO>>> GetProjectsFromDbAsync()
  {
    var projects = await _context.Projects.Select(p => new ProjectDTO
    {
      Id = p.Id,
      Name = p.Name
    }).ToListAsync();

    return Ok(projects);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Project>> GetSingleProjectFromDbAsync(int id)
  {
    var project = await _context.Projects.FindAsync(id);
    return Ok(project);
  }

  [HttpPost]
  public async Task<ActionResult<Project>> CreateProjectToDbAsync([FromBody] ProjectDTO projectDto)
  {
    var project = new Project
    {
      Name = projectDto.Name
    };

    await _context.Projects.AddAsync(project);
    await _context.SaveChangesAsync();
    return Ok(project);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<Project>> DeleteProjectFromDbAsync(int id)
  {
   var projectToDelete = await _context.Projects.FindAsync(id);
   if (projectToDelete is null)
   {
     return NotFound("Project not found");
   }

   _context.Projects.Remove(projectToDelete);
   await _context.SaveChangesAsync();
   return Ok(projectToDelete);
  }
}
