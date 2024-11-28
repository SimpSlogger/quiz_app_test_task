using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestsManager.Core.Models;

namespace TestsManager.DataAccess.Converters;

public class QuestionJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(QuestionBase).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, 
        Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        
        string? type = (string?)jo["QuestionType"];

        QuestionBase question;
        switch (type)
        {
            case "ChoiceQuestion":
                question = new ChoiceQuestion();
                break;
            default:
                throw new JsonException("Не удалось определить тип вопроса при чтении из JSON");
        }
        
        serializer.Populate(jo.CreateReader(), question);

        return question;
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, 
        object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
