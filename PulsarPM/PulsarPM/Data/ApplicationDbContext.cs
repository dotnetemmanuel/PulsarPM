using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PulsarPM.Data;

using DAL;
using Microsoft.AspNetCore.Identity;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
  public DbSet<Project> Projects { get; set; }
  public DbSet<KanbanBoard> KanbanBoards { get; set; }
  public DbSet<Card> Cards { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<KanbanBoard>()
      .HasMany(k => k.Cards)
      .WithOne(c => c.KanbanBoard)
      .HasForeignKey(c => c.KanbanBoardId)
      .OnDelete(DeleteBehavior.Cascade);

    base.OnModelCreating(modelBuilder);
  }
}
