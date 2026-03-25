using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the Container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//configure Http request pipeline
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
