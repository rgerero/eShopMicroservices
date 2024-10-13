using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

//add sevices to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("DB")!);
}).UseLightweightSessions();

var app = builder.Build();

//configure http request pipeline
app.MapCarter();

app.Run();
