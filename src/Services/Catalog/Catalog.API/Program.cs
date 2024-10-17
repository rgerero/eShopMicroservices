using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

//add sevices to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("DB")!);
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
	builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

//configure http request pipeline
app.MapCarter();
app.UseExceptionHandler(opton => { });

app.Run();
