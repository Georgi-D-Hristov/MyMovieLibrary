using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Models; // Добави using за твоите модели

namespace MyMovieLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Тази опция ще "инжектираме" от Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Това казва на EF Core: "Искам таблица, наречена Movies,
        // която да използва моя Movie модел."
        public DbSet<Movie> Movies { get; set; }
    }
}
