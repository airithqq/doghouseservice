using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestTask.API.Filters
{
    public class DogOrderFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if(parameter.Name == "order")
            {
                parameter.Schema.Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("asc"),
                    new OpenApiString("desc")
                };
            }
        }
    }
}
