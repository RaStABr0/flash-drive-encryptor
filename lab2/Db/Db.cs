using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace lab2
{
    public sealed class Db : DbContext
    {
        public DbSet<UsbDrive> UsbDrives { get; set; }

        public Db()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=devices.db");
        }
    }
}