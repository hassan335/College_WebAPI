using CollegeApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(t => t.DepartmentId);
            builder.Property(t => t.DepartmentId).UseIdentityColumn();
            builder.Property(x => x.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);
            

            builder.HasData(new List<Department>()
           {
               {
                   new Department {DepartmentId=1,DepartmentName="CS",Description = "College of Computing and Information"}
               },
                {
               new Department {DepartmentId=2,DepartmentName="Buisness",Description = "College of Buisness and Commerece"}
               },
           });


        }



    }
}
