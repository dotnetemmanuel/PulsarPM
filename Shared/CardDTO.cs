namespace Shared;

using System.ComponentModel.DataAnnotations;

public class CardDTO
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
  public string? Description { get; set; }
  public string Status { get; set; }
  public string? Color { get; set; }

  //Nav-properties
  public int KanbanBoardId { get; set; }
  public KanbanBoardDTO KanbanBoard { get; set; }
}
