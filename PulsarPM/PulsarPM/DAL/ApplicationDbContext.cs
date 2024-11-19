namespace PulsarPM.DAL;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
  public DbSet<Project> Projects { get; set; }
  public DbSet<KanbanBoard> KanbanBoards { get; set; }
  public DbSet<Card> Cards { get; set; }

}
