using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PulsarPM.Data;

using DAL;
using Microsoft.AspNetCore.Identity;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
  public DbSet<Project> Projects { get; set; }
  public DbSet<Card> Cards { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Project>()
      .HasMany(k => k.Cards)
      .WithOne(c => c.Project)
      .HasForeignKey(c => c.ProjectId)
      .OnDelete(DeleteBehavior.Cascade);

    base.OnModelCreating(modelBuilder);
  }
}
