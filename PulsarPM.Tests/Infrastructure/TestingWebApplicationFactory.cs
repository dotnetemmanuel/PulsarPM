using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PulsarPM.Tests.Infrastructure;

using DAL;
using Data;
using Microsoft.AspNetCore.Hosting;

public class TestingWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services => {

      var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

      if (dbContextDescriptor != null)
      {
        services.Remove(dbContextDescriptor);
      }

      services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase($"PulsarPM_TestDB"); });

      var sp = services.BuildServiceProvider();

      using (var scope = sp.CreateScope())
      {
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        try
        {
          db.Database.EnsureCreated();
          db.Projects.RemoveRange(db.Projects);
          db.Cards.RemoveRange(db.Cards);
          db.SaveChanges();
          // Add test data
          var project = new Project
          {
            Name = "Test Project"
          };
          db.Projects.Add(project);
          db.SaveChanges();

          var cards = new[]
          {
            new Card
            {
              Name = "Test Card 1",
              Description = "Test Description 1",
              ProjectId = project.Id,
              Status = "Backlog",
              Color = "Blue",
              Order = 0
            },
            new Card
            {
              Name = "Test Card 2",
              Description = "Test Description 2",
              ProjectId = project.Id,
              Status = "Backlog",
              Color = "Blue",
              Order = 1
            }
          };

          db.Cards.AddRange(cards);
          db.SaveChanges();
        }
        catch (Exception ex)
        {
          Console.WriteLine($"An error occurred seeding the database. Error: {ex.Message}");
        }
      }
    });
  }
}
