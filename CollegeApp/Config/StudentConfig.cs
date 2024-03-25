using CollegeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CollegeApp.Config
{
    public class StudentConfig:IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Student>()
           {
               {
                   new Student {Id=1,Name="Hassan",Address="NK",Email="Hsn@gmail.com",DOB= new DateTime(2024,12,12)}
               },
                {
                   new Student {Id=2,Name="Maaz",Address="Dastagir",Email="Mz@gmail.com",DOB= new DateTime(2024,12,13)}
               }
           });


        }



    }
}
