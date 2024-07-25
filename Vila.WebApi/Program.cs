using Microsoft.EntityFrameworkCore;
using Vila.WebApi.Context;
using Vila.WebApi.Services.Vila;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var local = builder.Configuration.GetConnectionString("Local");

services.AddControllers();
services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(local);
});

#region Dependency 
    services.AddTransient<IVilaService, VilaService>();
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseAuthorization();

app.MapControllers();

app.Run();
