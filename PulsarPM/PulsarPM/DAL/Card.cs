namespace PulsarPM.DAL;

using System.ComponentModel.DataAnnotations;

public class Card
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
  public string? Description { get; set; }
  public string Status { get; set; }
  public string? Color { get; set; }
  public int Order { get; set; }

  //Nav-properties
  public int ProjectId { get; set; }
  public Project? Project { get; set; }
}
