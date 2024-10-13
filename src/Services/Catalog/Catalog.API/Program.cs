using BuildingBlocks.Behaviours;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;
using static System.Net.Mime.MediaTypeNames;

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

app.UseExceptionHandler(exceptionHandlerApp =>
{
	exceptionHandlerApp.Run(async context =>
	{
		var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
		if (exception == null) { return; }

		var problemDetails = new ProblemDetails
		{
			Title = exception.Message,
			Status = StatusCodes.Status500InternalServerError,
			Detail = exception.StackTrace
		};

		var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
		logger.LogError(exception, exception.Message);
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		context.Response.ContentType= "application/json";

		await context.Response.WriteAsJsonAsync(problemDetails);
	});
});


app.Run();
