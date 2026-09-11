using AddressBookAssessment.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAssessment.Services.Data;

public class AddressBookAssessmentContext(DbContextOptions<AddressBookAssessmentContext> options) : DbContext(options)
{
	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);

		configurationBuilder.Properties(typeof(StringValueObject<>), builder =>
		{
			builder.HaveConversion<string>()
				.HaveMaxLength(450);
		});

		configurationBuilder.Properties<string>()
			.HaveMaxLength(450);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AddressBookEntryConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}
