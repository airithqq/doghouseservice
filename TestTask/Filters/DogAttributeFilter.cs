using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestTask.API.Filters
{
    public class DogAttributeFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name == "attribute")
            {
                parameter.Schema.Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("tailLength"),
                    new OpenApiString("weight")
                };
            }
        }
    }
}
