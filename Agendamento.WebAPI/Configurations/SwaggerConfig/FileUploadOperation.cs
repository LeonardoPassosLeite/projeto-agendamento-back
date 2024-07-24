using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Agendamento.WebAPI.Swagger
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var uploadFileParameters = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile))
                .ToList();

            if (uploadFileParameters.Any())
            {
                operation.Parameters = new List<OpenApiParameter>();

                foreach (var fileParameter in uploadFileParameters)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = fileParameter.Name,
                        In = ParameterLocation.Header,
                        Description = "Upload File",
                        Required = true,
                        Schema = new OpenApiSchema { Type = "file" }
                    });
                }

                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = uploadFileParameters.ToDictionary(p => p.Name, p => new OpenApiSchema { Type = "string", Format = "binary" })
                            }
                        }
                    }
                };
            }
        }
    }
}
