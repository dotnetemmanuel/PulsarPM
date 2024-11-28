namespace PulsarPM.DAL;

using System.ComponentModel.DataAnnotations;

public class Project
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
  public KanbanBoard? KanbanBoard { get; set; }
  public bool IsArchived { get; set; } = false;
  
  public List<Card> Cards { get; set; } = new();
}
