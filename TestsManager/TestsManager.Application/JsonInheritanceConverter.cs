using Newtonsoft.Json.Linq;
using NJsonSchema.Converters;

namespace TestsManager.Application;

public class JsonTestsManagerInheritanceConverter : JsonInheritanceConverter
{
    public JsonTestsManagerInheritanceConverter(Type type, string discriminatorName) : base(type, discriminatorName)
    {
    }

    protected override Type GetDiscriminatorType(JObject jObject, Type objectType, string discriminatorValue)
    {
        return objectType.Assembly.GetTypes()
                   .FirstOrDefault(f => f.IsSubclassOf(objectType)
                                        && string.Equals(f.Name, discriminatorValue,
                                            StringComparison.CurrentCultureIgnoreCase))
               ?? base.GetDiscriminatorType(jObject, objectType, discriminatorValue);
    }
}
