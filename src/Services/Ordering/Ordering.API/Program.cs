using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//app services to the cotainer
builder.Services
	.AddApplicationServices()
	.AddInfrastructureServices(builder.Configuration)
	.AddAPIServices();

var app = builder.Build();
//configure the http request pipeline

app.Run();
