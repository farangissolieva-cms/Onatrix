
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.CreateUmbracoBuilder()
	.AddBackOffice()
	.AddWebsite()
	.AddDeliveryApi()
	.AddComposers()
	// umbraco storage
    .AddAzureBlobMediaFileSystem(options =>
    {
        options.ConnectionString = configuration["Umbraco:Storage:AzureBlob:Media:ConnectionString"]!;
        options.ContainerName = configuration["Umbraco:Storage:AzureBlob:Media:ContainerName"]!;
    })
    .AddAzureBlobImageSharpCache()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
	.WithMiddleware(u =>
	{
		u.UseBackOffice();
		u.UseWebsite();
	})
	.WithEndpoints(u =>
	{
		u.UseBackOfficeEndpoints();
		u.UseWebsiteEndpoints();
	});

await app.RunAsync();
