using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AutoMapper;

namespace Rocket.Surgery.Operational.MediatR
{
    [ExcludeFromCodeCoverage]
    internal class JsonElementConverter :
        ITypeConverter<JsonElement, byte[]?>, ITypeConverter<byte[]?, JsonElement>,
        ITypeConverter<JsonElement, string?>, ITypeConverter<string?, JsonElement>,
        ITypeConverter<JsonElement?, byte[]?>, ITypeConverter<byte[]?, JsonElement?>,
        ITypeConverter<JsonElement?, string?>, ITypeConverter<string?, JsonElement?>
    {
        private static readonly JsonElement _empty = JsonSerializer.Deserialize<JsonElement>("{}");
        private static JsonElement GetDefault(JsonElement value) => JsonMapperOptions.DefaultValue switch
        {
            JsonDefaultValue.NotNull => value.ValueKind == JsonValueKind.Undefined ? _empty : value,
            _ => value
        };
        private static JsonElement GetDefault(JsonElement? value) => JsonMapperOptions.DefaultValue switch
        {
            JsonDefaultValue.NotNull => !value.HasValue || value.Value.ValueKind == JsonValueKind.Undefined ? _empty : value.Value,
            _ => value ?? default
        };

        public byte[]? Convert(JsonElement source, byte[]? destination, ResolutionContext context) =>
            source.ValueKind == JsonValueKind.Undefined
                ? destination ?? Array.Empty<byte>()
                : JsonSerializer.SerializeToUtf8Bytes(source);

        public JsonElement Convert(byte[]? source, JsonElement destination, ResolutionContext context) =>
            source == null || source.Length == 0
                ? GetDefault(destination)
                : JsonSerializer.Deserialize<JsonElement>(source);

        public string? Convert(JsonElement source, string? destination, ResolutionContext context) =>
            source.ValueKind == JsonValueKind.Undefined
                ? destination ?? string.Empty
                : JsonSerializer.Serialize(source);

        public JsonElement Convert(string? source, JsonElement destination, ResolutionContext context) =>
            string.IsNullOrEmpty(source)
                ? GetDefault(destination)
                : JsonSerializer.Deserialize<JsonElement>(source);

        public byte[]? Convert(JsonElement? source, byte[]? destination, ResolutionContext context) =>
            !source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined
                ? destination ?? Array.Empty<byte>()
                : JsonSerializer.SerializeToUtf8Bytes(source);

        public JsonElement? Convert(byte[]? source, JsonElement? destination, ResolutionContext context) =>
            source == null || source.Length == 0
                ? GetDefault(destination)
                : JsonSerializer.Deserialize<JsonElement>(source);

        public string? Convert(JsonElement? source, string? destination, ResolutionContext context) =>
            !source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined
                ? string.Empty
                : JsonSerializer.Serialize(source);

        public JsonElement? Convert(string? source, JsonElement? destination, ResolutionContext context) =>
            string.IsNullOrEmpty(source)
                ? GetDefault(destination)
                : JsonSerializer.Deserialize<JsonElement>(source);
    }
}