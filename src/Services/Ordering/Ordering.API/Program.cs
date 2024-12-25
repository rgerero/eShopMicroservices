using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//app services to the cotainer
builder.Services
	.AddApplicationServices()
	.AddInfrastructureServices(builder.Configuration)
	.AddAPIServices(builder.Configuration);

var app = builder.Build();

//configure the http request pipeline
app.UseAPIServices();

if(app.Environment.IsDevelopment())
{
	await app.InitializeDatabaseAsync();
}
app.Run();
