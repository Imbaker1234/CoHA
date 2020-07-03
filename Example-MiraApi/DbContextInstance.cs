namespace MiraThree
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;

    public class DbContextInstance : DbContext
    {
        public DbSet<Student> Students { get; set; } 
        private string _db { get; set; }

        protected DbContextInstance(DbSet<Student> students, string db)
        {
            Students = students;
            _db = db;
        }

        public DbContextInstance(string db)
        {
            _db = db;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_db}", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}