using AddressBookAssessment.Services.Data;
using AddressBookAssessment.Services.Web.Api;
using AddressBookAssessment.Services.Web.AspNet;
using AddressBookAssessment.Services.Web.Auth;
using AddressBookAssessment.Services.Web.Mediator;
using AddressBookAssessment.Services.Web.OpenApi;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAuth();
builder.Services.AddSingleton(TimeProvider.System);
builder.ConfigureMediator(services => services.AddMediator(options =>
{
	options.ServiceLifetime = ServiceLifetime.Transient;
}));

var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("addressbookassessment"))
{
	ApplicationName = "addressbookassessment"
};

builder.ConfigureDatabase(connectionStringBuilder.ConnectionString);

builder.ConfigureAspNet();
builder.ConfigureOpenApi();

var app = builder.Build();

app.MapApi();
app.MapOpenApiEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();