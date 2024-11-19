namespace PulsarPM.DAL;

using System.ComponentModel.DataAnnotations;

public class KanbanBoard
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  //Nav-properties
  public int ProjectId { get; set; }
  public Project Project { get; set; }
  public List<Card> Cards { get; set; } = new();
}
