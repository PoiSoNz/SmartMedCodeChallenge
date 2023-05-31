using Microsoft.EntityFrameworkCore;
using SmartMedCodeChallenge.Models;

namespace SmartMedCodeChallenge.Data
{
    public class MedicationDbContext: DbContext
    {
        public MedicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent Entity Framework error caused by Medication INSERT/UPDATE SQL trigger
            modelBuilder.Entity<Medication>().ToTable(tb => tb.HasTrigger("tgMedicationInsertOrUpdateCreationDate"));
        }
    }
}
