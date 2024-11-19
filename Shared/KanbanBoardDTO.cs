namespace Shared;

using System.ComponentModel.DataAnnotations;

public class KanbanBoardDTO
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  //Nav-properties
  public int ProjectId { get; set; }
  public ProjectDTO Project { get; set; }
  public List<CardDTO> Cards { get; set; } = new();
}
