using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rocket.Surgery.Operational.MediatR.NewtonsoftJson
{
    [ExcludeFromCodeCoverage]
    class JTokenConverter :
        ITypeConverter<JToken, byte[]?>,
        ITypeConverter<byte[]?, JToken?>,
        ITypeConverter<JToken?, string?>,
        ITypeConverter<string?, JToken?>,
        ITypeConverter<JToken?, JToken?>,
        ITypeConverter<JArray?, byte[]?>,
        ITypeConverter<byte[]?, JArray?>,
        ITypeConverter<JArray?, string?>,
        ITypeConverter<string?, JArray?>,
        ITypeConverter<JArray?, JArray?>,
        ITypeConverter<JObject?, byte[]?>,
        ITypeConverter<byte[]?, JObject?>,
        ITypeConverter<JObject?, string?>,
        ITypeConverter<string?, JObject?>,
        ITypeConverter<JObject?, JObject?>
    {
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
                return destination;
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
                return destination;
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
                return destination ?? new JArray();
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
                return destination ?? new JArray();
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
                return destination ?? new JObject();
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
                return destination ?? new JObject();
            }
        }

        public JToken? Convert(JToken? source, JToken? destination, ResolutionContext context) => source ?? destination;

        public JArray? Convert(JArray? source, JArray? destination, ResolutionContext context) => source ?? destination;

        public JObject? Convert(JObject? source, JObject? destination, ResolutionContext context)
            => source ?? destination;

        private byte[] WriteToBytes(JToken source)
        {
            var memory = new MemoryStream();
            using var sw = new StreamWriter(memory);
            var jw = new JsonTextWriter(sw) { Formatting = Formatting.None };
            source.WriteTo(jw);
            jw.Flush();
            memory.Position = 0;
            return memory.ToArray();
        }
    }
}