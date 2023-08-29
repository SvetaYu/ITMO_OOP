using Application.Dto;
using Newtonsoft.Json;
using Presentation.Models;

namespace Presentation.Converters;

public class CreateMessageSourceModelJsonConverter : JsonConverter<CreateMessageSourceModel>
{
    private readonly JsonSerializer _serializer;

    public CreateMessageSourceModelJsonConverter()
    {
        _serializer = JsonSerializer.CreateDefault();
    }
    
    public override void WriteJson(JsonWriter writer, CreateMessageSourceModel? value, JsonSerializer serializer)
    {
        _serializer.Serialize(writer, value);
    }

    public override CreateMessageSourceModel? ReadJson(JsonReader reader, Type objectType, CreateMessageSourceModel ? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        A? a = serializer.Deserialize<A>(reader);

        MessageSourceNameDto name = a.Type switch
        {
            MessageTypeDto.Phone => JsonConvert.DeserializeObject<PhoneNameDto>(JsonConvert.SerializeObject(a.Name)),
            MessageTypeDto.Email => JsonConvert.DeserializeObject<EmailNameDto>(JsonConvert.SerializeObject(a.Name)),
            _ => throw new ArgumentOutOfRangeException()
        };

        return new CreateMessageSourceModel(a.Type, name);
    }

    private record A(MessageTypeDto Type, object Name);
}