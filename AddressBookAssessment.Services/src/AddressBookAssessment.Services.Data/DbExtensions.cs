using AddressBookAssessment.Core.Shared;
using AddressBookAssessment.Services.Data.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AddressBookAssessment.Services.Data;

public static class DbExtensions
{
	public static void ConfigureDatabase(this WebApplicationBuilder builder, string connectionString)
	{
		builder.AddSqlServerDbContext<AddressBookAssessmentContext>("sqlserver", options =>
		{
			// The default value allows 'dotnet ef migrations bundles' to build.
			options.ConnectionString = connectionString ?? "DEFAULT";
		});

		builder.Services.AddScoped<IReadModel, EfReadModel<AddressBookAssessmentContext>>();
		builder.Services.AddScoped<IWorkspace, EfWorkspace<AddressBookAssessmentContext>>();
		builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork<AddressBookAssessmentContext>>();
	}

	public static async Task InitializeDatabase(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dbContext = serviceScope.ServiceProvider.GetRequiredService<AddressBookAssessmentContext>();
			var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<AddressBookAssessmentContext>>();

			for (var attempt = 1; ; attempt++)
			{
				try
				{
					await dbContext.Database.MigrateAsync();
					break;
				}
				catch (Exception ex) when (attempt < 5)
				{
					logger.LogWarning(ex, "Migration attempt {Attempt} failed, retrying in 10s...", attempt);
					await Task.Delay(TimeSpan.FromSeconds(10));
				}
			}
		}
	}
}
