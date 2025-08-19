using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolApplication.Entities.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="Application"/>
    /// </summary>
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Applications");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId);
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.Reason).IsRequired().HasMaxLength(255);
        }
    }
}
