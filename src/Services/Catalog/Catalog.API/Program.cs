using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);

//add sevices to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("DB")!);
}).UseLightweightSessions();

var app = builder.Build();

//configure http request pipeline
app.MapCarter();

app.Run();
