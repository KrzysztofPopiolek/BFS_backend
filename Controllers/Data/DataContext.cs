using BFS_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<EventDetails> EventDetails { get; set; }
        public virtual DbSet<BusinessOwnerDetails> BusinessOwnerDetails { get; set; }
        public virtual DbSet<ContractorDetailsConst> ContractorDetailsConsts { get; set; }
        public virtual DbSet<TaxYearDatesDetails> TaxYearDatesDetails { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<MileageRecord> MileageRecords { get; set; }
        public virtual DbSet<MonthlyStatement> MonthlyStatements { get; set; }
        public virtual DbSet<VehicleDetails> VehicleDetails { get; set; }
    }
}