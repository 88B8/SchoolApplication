using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApplication.Entities.Contracts.ValidationRules;

namespace SchoolApplication.Entities.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="Parent"/>
    /// </summary>
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.ToTable("Parents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Surname)
                .HasMaxLength(ParentValidationRules.SurnameMaxLength);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ParentValidationRules.SurnameMaxLength);

            builder.Property(x => x.Patronymic)
                .HasMaxLength(ParentValidationRules.SurnameMaxLength);
        }
    }
}
