var builder = WebApplication.CreateBuilder(args);

//app services to the cotainer

var app = builder.Build();

//configure the http request pipeline

app.Run();
