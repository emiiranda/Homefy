using Homefy.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

app.ConfigurePipeline();

app.Run();