namespace AddressBookAssessment.Services.Web.Api;

public static class ApiExtensions
{
	public static void MapApi(this WebApplication app)
	{
		app.UseExceptionHandler();

		var api = app.MapGroup("api");

		api.MapAddressBookEndpoints();
	}
}
