using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
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

                // Verificar si la propiedad es una lista o colección
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyInfo.PropertyType)
                    && propertyInfo.PropertyType != typeof(string))
                {
                    // Asegurarse de que la propiedad sea reconocida como una lista
                    var collectionItems = schema.Properties[property.Key].Items;
                    if (collectionItems != null)
                    {
                        collectionItems.Type = "array"; // Especificar que es un array en Swagger.

                        // Si los elementos de la lista son de tipo complejo (como EntZonas), definimos su tipo
                        if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            // Identificar el tipo de los elementos de la lista
                            var elementType = propertyInfo.PropertyType.GetGenericArguments()[0];
                            collectionItems.Reference = new OpenApiReference
                            {
                                Type = ReferenceType.Schema,
                                Id = elementType.Name
                            };
                        }
                    }
                }
            }
        }
    }
}
