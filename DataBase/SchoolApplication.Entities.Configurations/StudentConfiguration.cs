using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolApplication.Entities.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="School"/>
    /// </summary>
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.ParentId).IsRequired();
            builder.Property(x => x.SchoolId).IsRequired();
            builder.Property(x => x.Grade).IsRequired().HasMaxLength(5);
            builder
                .HasOne(x => x.Parent)
                .WithMany()
                .HasForeignKey(x => x.ParentId);
        }
    }
}
