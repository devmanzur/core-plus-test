using CorePlus.Modules.Appointments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorePlus.Modules.Appointments.Persistence.Configurations;

public class PractitionerConfiguration : IEntityTypeConfiguration<Practitioner>
{
    public void Configure(EntityTypeBuilder<Practitioner> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Practitioner)
            .HasForeignKey(a => a.PractitionerId);
            
        builder.Metadata.FindNavigation(nameof(Practitioner.Appointments))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}