using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Vila.WebApi.Context;
using Vila.WebApi.Mappings;
using Vila.WebApi.Services.Detail;
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
services.AddTransient<IDetailService, DetailService>();
#endregion

#region AutoMapper
services.AddAutoMapper(typeof(ModelsMapper));
#endregion

#region Versioning
services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
});
#endregion

#region Swagger
services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("VilaOpenApi",
        new OpenApiInfo
        {
            Title = "Vila Api",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "Sina Ghaffari",
                Email = "Sinaghaffari.dev@gmail.com"
            }

        });

    var pathComment = Path.Combine(AppContext.BaseDirectory, "SwaggerComments.xml");
    option.IncludeXmlComments(pathComment);
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json", "Vila Open Api");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
