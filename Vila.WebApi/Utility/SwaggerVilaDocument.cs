using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Vila.WebApi.Utility
{
    public class SwaggerVilaDocument : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public SwaggerVilaDocument(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName,
                new OpenApiInfo
                {
                    Title = $"Vila Api Version {item.ApiVersion}",
                    Version = item.ApiVersion.ToString(),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Sina Ghaffari",
                    //    Email = "Sinaghaffari.dev@gmail.com"
                    //}

                });
            }


            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter 'Bearer' [space] and then your token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
             {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });


            var pathComment = Path.Combine(AppContext.BaseDirectory, "SwaggerComments.xml");
            options.IncludeXmlComments(pathComment);
        }

    }
}
 