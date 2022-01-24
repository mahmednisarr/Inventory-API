using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Core
{
    public class Header : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "CompName",
            //    In = ParameterLocation.Header,
            //    Required = true,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "string"
            //    }
            //});

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "Auth",
            //    In = ParameterLocation.Header,
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "string"
            //    }
            //});
        }
    }
}
