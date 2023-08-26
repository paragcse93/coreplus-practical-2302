using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Coreplus.Sample.Api.utils
{
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat;

        public JsonDateTimeConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParseExact(reader.GetString(), _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return DateTime.MinValue; // Return a default value on conversion failure
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat, CultureInfo.InvariantCulture));
        }
    }

}
