using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> option) : DbContext(option)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    //method that will be executed as the migration executes
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting"},
            new { Id = 2, Name = "Arcade"},
            new { Id = 3, Name = "Racing"},
            new { Id = 4, Name = "Sports"},
            new { Id = 5, Name = "Adventure"}
        );
    }
}