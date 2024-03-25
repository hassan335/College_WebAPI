using CollegeApp.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDbContext:DbContext
    {
       public DbSet<Student> Students { get; set; }



        public CollegeDbContext(DbContextOptions<CollegeDbContext> options):base (options)
        {
                    
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Student>().HasData(new List<Student>()
            //{
            //    {
            //        new Student {Id=1,Name="Hassan",Address="NK",Email="Hsn@gmail.com",DOB= new DateTime(2024,12,12)}
            //    },
            //     {
            //        new Student {Id=2,Name="Maaz",Address="Dastagir",Email="Mz@gmail.com",DOB= new DateTime(2024,12,13)}
            //    }
            //});

            modelBuilder.ApplyConfiguration(new StudentConfig());



            modelBuilder.Entity<Student>(n =>
            {
                n.Property(x => x.Name).IsRequired().HasMaxLength(250);
                n.Property(x => x.Email).IsRequired().HasMaxLength(250);
                n.Property(x => x.Address).IsRequired(false).HasMaxLength(500);


            });
        }


    }
}
