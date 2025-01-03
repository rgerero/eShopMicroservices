using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

//app services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//db services
builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("DB")!);
	opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//grpc services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts =>
{
	opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
//this bypassing of ssl cert is for development purposes only.	
.ConfigurePrimaryHttpMessageHandler(() =>
{
	var handler = new HttpClientHandler
	{
		ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
	};
	return handler;
});

//async communication service
builder.Services.AddMessageBroker(builder.Configuration);

//cross-cutting services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("DB")!)
	.AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
	new HealthCheckOptions
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});



app.Run();
