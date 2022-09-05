using DataBaseHelper.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBaseHelper
{
    public class AppContext : DbContext
    {
        public DbSet<Visit> Visits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=filesHistory.db");
        }
    }
}
