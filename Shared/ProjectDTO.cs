namespace Shared;

using System.ComponentModel.DataAnnotations;

public class ProjectDTO
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
  public List<CardDTO> Cards { get; set; } = new();
  public bool IsArchived { get; set; } = false;
}
