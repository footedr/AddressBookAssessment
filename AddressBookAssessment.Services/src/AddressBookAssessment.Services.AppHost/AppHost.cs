var builder = DistributedApplication.CreateBuilder(args);

var saPassword = builder.AddParameter("sa-password");

var sql = builder.AddSqlServer("sqlserver", password: saPassword, 56111)
	.WithLifetime(ContainerLifetime.Persistent)
	.WithDataVolume()
	.AddDatabase("addressbookassessment");

builder.AddProject<Projects.AddressBookAssessment_Services_Web>("addressbookassessment-services-web")
	.WithUrl("/scalar/v1", "Open API Docs")
	.WithReference(sql)
	.WaitFor(sql);

builder.Build().Run();
