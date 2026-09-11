using AddressBookAssessment.Core.Shared;
using AddressBookAssessment.Services.Data.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
}
