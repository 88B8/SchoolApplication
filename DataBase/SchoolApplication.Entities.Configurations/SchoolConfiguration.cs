using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApplication.Entities.Contracts.ValidationRules;

namespace SchoolApplication.Entities.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="School"/>
    /// </summary>
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("Schools");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(SchoolValidationRules.NameMaxLength)
                .IsRequired();
            builder.Property(x => x.DirectorName)
                .HasMaxLength(SchoolValidationRules.DirectorNameMaxLength)
                .IsRequired();
        }
    }
}