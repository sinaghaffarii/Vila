using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vila.WebApi.Context;
using Vila.WebApi.Mappings;
using Vila.WebApi.Services.Customer;
using Vila.WebApi.Services.Detail;
using Vila.WebApi.Services.Vila;
using Vila.WebApi.Utility;

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
services.AddTransient<ICustomerService, CustomerService>();
#endregion

#region AutoMapper
services.AddAutoMapper(typeof(ModelsMapper));
#endregion

#region Versioning
services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
});

services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVVV";
});
#endregion

#region Swagger
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVilaDocument>();
services.AddSwaggerGen();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        var provider = app.Services.CreateScope().ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var item in provider.ApiVersionDescriptions)
        {
            x.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToString());
        }

        //x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json", "Vila Open Api");
        x.RoutePrefix = "";
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
