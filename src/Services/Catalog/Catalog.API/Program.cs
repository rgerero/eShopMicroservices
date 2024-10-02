var builder = WebApplication.CreateBuilder(args);

//add sevices to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

//configure http request pipeline
app.MapCarter();

app.Run();
