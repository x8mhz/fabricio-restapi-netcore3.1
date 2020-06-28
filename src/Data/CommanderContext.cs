using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> options) : base (options)
        {
            
        }

        public DbSet<Command> Commands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CatalogDB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}