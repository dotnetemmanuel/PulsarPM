namespace PulsarPM.DAL;

public class Project
{
  public int Id { get; set; }
  public string Name { get; set; }
  public KanbanBoard KanbanBoard { get; set; }
}
