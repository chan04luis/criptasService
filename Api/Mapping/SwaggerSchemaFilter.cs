using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Reflection;

public class SwaggerSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Creamos una nueva lista para almacenar las propiedades modificadas
        var properties = schema.Properties.ToList();

        // Reemplazamos las propiedades originales con los nuevos nombres
        foreach (var property in properties)
        {
            var propertyInfo = context.Type.GetProperty(property.Key);
            if (propertyInfo != null)
            {
                // Obtener el atributo JsonProperty
                var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonPropertyAttribute != null)
                {
                    // Cambiar el nombre de la propiedad en el diccionario
                    schema.Properties.Remove(property.Key);
                    schema.Properties.Add(jsonPropertyAttribute.PropertyName, property.Value);
                }
            }
        }
    }
}
