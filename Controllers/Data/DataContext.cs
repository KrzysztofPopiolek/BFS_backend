using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<BusinessOwnerDetails> BusinessOwnerDetails { get; set; }
        public DbSet<ContractorDetailsConst> ContractorDetailsConsts { get; set; }
    }
}