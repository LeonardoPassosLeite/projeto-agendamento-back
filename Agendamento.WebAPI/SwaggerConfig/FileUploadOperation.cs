using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class FileUploadOperation : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadMime = context.MethodInfo.GetCustomAttributes(true).OfType<ConsumesAttribute>().Any(x => x.ContentTypes.Contains("multipart/form-data"));
        if (!fileUploadMime) return;

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "file",
            In = ParameterLocation.Header,
            Description = "Upload Image",
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            }
        });
    }
}