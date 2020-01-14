using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NewtonsoftJsonSerializer = Newtonsoft.Json.JsonSerializer;
using JsonSerializer = System.Text.Json.JsonSerializer;
using JsonException = System.Text.Json.JsonException;

namespace Rocket.Surgery.Operational.MediatR.NewtonsoftJson
{
    [ExcludeFromCodeCoverage]
    class JTokenConverter :
        ITypeConverter<JToken, byte[]?>,
        ITypeConverter<byte[]?, JToken?>,
        ITypeConverter<JToken?, string?>,
        ITypeConverter<string?, JToken?>,
        ITypeConverter<JsonElement?, JToken?>,
        ITypeConverter<JToken?, JsonElement?>,
        ITypeConverter<JsonElement, JToken?>,
        ITypeConverter<JToken?, JsonElement>,
        ITypeConverter<JArray?, byte[]?>,
        ITypeConverter<byte[]?, JArray?>,
        ITypeConverter<JArray?, string?>,
        ITypeConverter<string?, JArray?>,
        ITypeConverter<JsonElement?, JArray?>,
        ITypeConverter<JArray?, JsonElement?>,
        ITypeConverter<JsonElement, JArray?>,
        ITypeConverter<JArray?, JsonElement>,
        ITypeConverter<JObject?, byte[]?>,
        ITypeConverter<byte[]?, JObject?>,
        ITypeConverter<JObject?, string?>,
        ITypeConverter<string?, JObject?>,
        ITypeConverter<JsonElement?, JObject?>,
        ITypeConverter<JObject?, JsonElement?>,
        ITypeConverter<JsonElement, JObject?>,
        ITypeConverter<JObject?, JsonElement>
    {
        private static readonly JsonElement _empty = JsonSerializer.Deserialize<JsonElement>("{}");
        private static JsonElement GetDefaultSjt(JsonElement value) => JsonMapperOptions.DefaultValue switch
        {
            JsonDefaultValue.NotNull => value.ValueKind == JsonValueKind.Undefined ? _empty : value,
            _ => value
        };
        private static JsonElement GetDefaultSjt(JsonElement? value) => JsonMapperOptions.DefaultValue switch
        {
            JsonDefaultValue.NotNull => !value.HasValue || value.Value.ValueKind == JsonValueKind.Undefined ? _empty : value.Value,
            _ => value ?? default
        };
        private static JToken? GetDefaultToken(JToken? value) => JsonMapperOptions.DefaultValue switch
        {
            JsonDefaultValue.NotNull => value ?? new JObject(),
            _ => value ?? default
        };
        private static T? GetDefault<T>(T? value) where T : JToken, new() => JsonMapperOptions.DefaultValue switch
        {
            JsonDefaultValue.NotNull => value ?? new T(),
            _ => value
        };

        public byte[]? Convert(JToken? source, byte[]? destination, ResolutionContext context)
        {
            if (source == null || source.Type == JTokenType.None)
                return destination ?? Array.Empty<byte>();
            return WriteToBytes(source);
        }


        public JToken? Convert(byte[]? source, JToken? destination, ResolutionContext context)
        {
            try
            {
                return source == null || source.Length == 0
                    ? destination
                    : JToken.Parse(Encoding.UTF8.GetString(source));
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination);
            }
        }


        public string? Convert(JToken? source, string? destination, ResolutionContext context)
            => source?.ToString(Formatting.None) ?? destination;


        public JToken? Convert(string? source, JToken? destination, ResolutionContext context)
        {
            try
            {
                return string.IsNullOrEmpty(source) ? destination : JToken.Parse(source);
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination);
            }
        }

        public byte[]? Convert(JArray? source, byte[]? destination, ResolutionContext context)
        {
            if (source == null || source.Type == JTokenType.None)
                return destination ?? Array.Empty<byte>();
            return WriteToBytes(source);
        }

        public JArray? Convert(byte[]? source, JArray? destination, ResolutionContext context)
        {
            try
            {
                return source == null || source.Length == 0
                    ? destination ?? new JArray()
                    : JArray.Parse(Encoding.UTF8.GetString(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public string? Convert(JArray? source, string? destination, ResolutionContext context)
            => source?.ToString(Formatting.None) ?? destination;

        public JArray? Convert(string? source, JArray? destination, ResolutionContext context)
        {
            try
            {
                return string.IsNullOrEmpty(source) ? destination ?? new JArray() : JArray.Parse(source);
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public byte[]? Convert(JObject? source, byte[]? destination, ResolutionContext context)
        {
            if (source == null || source.Type == JTokenType.None)
                return destination ?? Array.Empty<byte>();
            return WriteToBytes(source);
        }

        public JObject? Convert(byte[]? source, JObject? destination, ResolutionContext context)
        {
            try
            {
                return source == null || source.Length == 0
                    ? destination ?? new JObject()
                    : JObject.Parse(Encoding.UTF8.GetString(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public string? Convert(JObject? source, string? destination, ResolutionContext context)
            => source?.ToString(Formatting.None) ?? destination;

        public JObject? Convert(string? source, JObject? destination, ResolutionContext context)
        {
            try
            {
                return string.IsNullOrEmpty(source) ? destination ?? new JObject() : JObject.Parse(source);
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public JsonElement Convert(JObject? source, JsonElement destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination);
            }
            using var data = WriteToStream(source);
            try
            {
                using var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination);
            }
        }

        public JObject? Convert(JsonElement source, JObject? destination, ResolutionContext context)
        {
            if (source.ValueKind == JsonValueKind.Undefined) return GetDefault(destination);
            try
            {
                return JObject.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public JsonElement? Convert(JObject? source, JsonElement? destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination);
            }
            using var data = WriteToStream(source);
            try
            {
                using var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination);
            }
        }

        public JObject? Convert(JsonElement? source, JObject? destination, ResolutionContext context)
        {
            if (!source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined) GetDefault(destination);
            try
            {
                return JObject.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public JsonElement Convert(JArray? source, JsonElement destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination);
            }
            using var data = WriteToStream(source);
            try
            {
                using var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination);
            }
        }

        public JArray? Convert(JsonElement source, JArray? destination, ResolutionContext context)
        {
            if (source.ValueKind == JsonValueKind.Undefined) GetDefault(destination);
            try
            {
                return JArray.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public JsonElement? Convert(JArray? source, JsonElement? destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination);
            }
            using var data = WriteToStream(source);
            try
            {
                using var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination);
            }
        }

        public JArray? Convert(JsonElement? source, JArray? destination, ResolutionContext context)
        {
            if (!source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined) GetDefault(destination);
            try
            {
                return JArray.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination);
            }
        }

        public JsonElement Convert(JToken? source, JsonElement destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination);
            }
            using var data = WriteToStream(source);
            try
            {
                using var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination);
            }
        }

        public JToken? Convert(JsonElement source, JToken? destination, ResolutionContext context)
        {
            if (source.ValueKind == JsonValueKind.Undefined) return GetDefaultToken(destination);
            try
            {
                return JToken.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination);
            }
        }

        public JsonElement? Convert(JToken? source, JsonElement? destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination);
            }
            using var data = WriteToStream(source);
            try
            {
                using var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination);
            }
        }

        public JToken? Convert(JsonElement? source, JToken? destination, ResolutionContext context)
        {
            if (!source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined) return GetDefaultToken(destination);
            try
            {
                return JToken.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination);
            }
        }

        private byte[] WriteToBytes(JToken source)
        {
            using var memory = new MemoryStream();
            using var sw = new StreamWriter(memory);
            using var jw = new JsonTextWriter(sw) { Formatting = Formatting.None };
            source.WriteTo(jw);
            jw.Flush();
            memory.Position = 0;
            return memory.ToArray();
        }

        private Stream WriteToStream(JToken source)
        {
            var memory = new MemoryStream();
            using var sw = new StreamWriter(memory);
            using var jw = new JsonTextWriter(sw) { Formatting = Formatting.None };
            source.WriteTo(jw);
            jw.Flush();
            memory.Position = 0;
            return memory;
        }
    }
}