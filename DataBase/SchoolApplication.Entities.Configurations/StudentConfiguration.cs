using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApplication.Entities.Contracts.ValidationRules;

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

            builder.Property(x => x.Surname)
                .HasMaxLength(StudentValidationRules.SurnameMaxLength)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(StudentValidationRules.NameMaxLength)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(StudentValidationRules.PatronymicMaxLength)
                .IsRequired();

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x => x.Grade)
                .HasMaxLength(StudentValidationRules.GradeMaxLength)
                .IsRequired();
        }
    }
}
