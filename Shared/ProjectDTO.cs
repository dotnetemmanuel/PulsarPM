namespace Shared;

using System.ComponentModel.DataAnnotations;

public class ProjectDTO
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
  public KanbanBoardDTO KanbanBoard { get; set; }
  public bool IsArchived { get; set; }
}
