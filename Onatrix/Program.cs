
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.CreateUmbracoBuilder()
	.AddBackOffice()
	.AddWebsite()
	.AddDeliveryApi()
	.AddComposers()
    .AddAzureBlobMediaFileSystem(options =>
    {
        // Retrieve Azure Blob Storage settings from configuration
        options.ConnectionString = configuration["AzureBlob:Media:ConnectionString"]!;
        options.ContainerName = configuration["AzureBlob:Media:ContainerName"]!;
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
