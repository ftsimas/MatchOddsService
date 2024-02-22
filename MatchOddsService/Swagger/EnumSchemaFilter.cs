using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Xml.Linq;

namespace MatchOddsService.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        private readonly XDocument _xmlComments;

        public EnumSchemaFilter(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                _xmlComments = XDocument.Load(xmlPath);
            }
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (_xmlComments == null) return;

            if (context.Type.IsEnum)
            {
                schema.Description += "<p>Values:</p><ul>";
                foreach (var name in Enum.GetNames(context.Type))
                {
                    schema.Description += $"<li>{name} - {(int)Enum.Parse(context.Type, name)}</li>";
                }
                schema.Description += "</ul>";
            }
        }
    }
}
