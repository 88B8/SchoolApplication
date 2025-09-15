using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApplication.Entities.Contracts.ValidationRules;

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

            builder
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId);

            builder
                .HasOne(x => x.School)
                .WithMany()
                .HasForeignKey(x => x.SchoolId);

            builder
                .HasOne(x => x.Parent)
                .WithMany()
                .HasForeignKey(x => x.ParentId);

            builder.Property(x => x.Reason)
                .IsRequired()
                .HasMaxLength(ApplicationValidationRules.ReasonMaxLength);
        }
    }
}
