using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Entities;

namespace StudentManagementSystem.Context
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentInfo> Students { get; set; }
        public DbSet<StudentGroup> Groups { get; set; }
        public DbSet<Department> Departments { get; set; }

        #region Seed data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }

        public class DbInitializer
        {
            private readonly ModelBuilder modelBuilder;

            public DbInitializer(ModelBuilder modelBuilder)
            {
                this.modelBuilder = modelBuilder;
            }

            public void Seed()
            {
                modelBuilder.Entity<Department>().HasData(
                       new Department() { Id = 1, Name ="Department 1" },
                       new Department() { Id = 2, Name ="Department 2" },
                       new Department() { Id = 3, Name ="Department 3" },
                       new Department() { Id = 4, Name ="Department 4" }       

                );
            }
        }

        #endregion
    }
}
