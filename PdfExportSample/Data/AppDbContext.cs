using Microsoft.EntityFrameworkCore;
using PdfExportSample.Model;

namespace PdfExportSample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PatientData> PatientTable { get; set; }
    }
}
