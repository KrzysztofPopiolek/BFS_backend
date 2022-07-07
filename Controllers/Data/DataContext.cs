using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet <EventDetails> EventDetails {get; set;}
    }
}