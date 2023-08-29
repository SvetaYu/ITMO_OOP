using Application.Dto;
using Newtonsoft.Json;
using Presentation.Models;

namespace Presentation.Converters;

public class CreateMessageModelJsonConverter : JsonConverter<CreateMessageDto>
{
    private readonly JsonSerializer _serializer;

    public CreateMessageModelJsonConverter()
    {
        _serializer = JsonSerializer.CreateDefault();
    }
    
    public override void WriteJson(JsonWriter writer, CreateMessageDto? value, JsonSerializer serializer)
    {
        _serializer.Serialize(writer, value);
    }

    public override CreateMessageDto? ReadJson(JsonReader reader, Type objectType, CreateMessageDto ? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        A? a = serializer.Deserialize<A>(reader);

        MessageContentDto content = a.Type switch
        {
            MessageTypeDto.Phone => JsonConvert.DeserializeObject<PhoneContentDto>(JsonConvert.SerializeObject(a.Content)),
            MessageTypeDto.Email => JsonConvert.DeserializeObject<EmailContentDto>(JsonConvert.SerializeObject(a.Content)),
            _ => throw new ArgumentOutOfRangeException()
        };

        return new CreateMessageDto(a.Type, content);
    }

    private record A(MessageTypeDto Type, object Content);
}