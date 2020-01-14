﻿using System.Text.Json;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace Rocket.Surgery.Operational.MediatR.NewtonsoftJson
{
    class NewtonsoftJsonMapper : Profile
    {
        public NewtonsoftJsonMapper()
        {
            var converter = new JTokenConverter();
            CreateMap<JArray?, byte[]?>().ConvertUsing(converter);
            CreateMap<JArray?, string?>().ConvertUsing(converter);
            CreateMap<byte[]?, JArray?>().ConvertUsing(converter);
            CreateMap<string?, JArray?>().ConvertUsing(converter);
            CreateMap<JObject?, byte[]?>().ConvertUsing(converter);
            CreateMap<JObject?, string?>().ConvertUsing(converter);
            CreateMap<byte[]?, JObject?>().ConvertUsing(converter);
            CreateMap<string?, JObject?>().ConvertUsing(converter);
            CreateMap<JToken?, byte[]?>().ConvertUsing(converter);
            CreateMap<JToken?, string?>().ConvertUsing(converter);
            CreateMap<byte[]?, JToken?>().ConvertUsing(converter);
            CreateMap<string?, JToken?>().ConvertUsing(converter);
            CreateMap<JsonElement?, JObject?>().ConvertUsing(converter);
            CreateMap<JObject?, JsonElement?>().ConvertUsing(converter);
            CreateMap<JsonElement, JObject?>().ConvertUsing(converter);
            CreateMap<JObject?, JsonElement>().ConvertUsing(converter);
            CreateMap<JsonElement?, JArray?>().ConvertUsing(converter);
            CreateMap<JArray?, JsonElement?>().ConvertUsing(converter);
            CreateMap<JsonElement, JArray?>().ConvertUsing(converter);
            CreateMap<JArray?, JsonElement>().ConvertUsing(converter);
            CreateMap<JsonElement?, JToken?>().ConvertUsing(converter);
            CreateMap<JToken?, JsonElement?>().ConvertUsing(converter);
            CreateMap<JsonElement, JToken?>().ConvertUsing(converter);
            CreateMap<JToken?, JsonElement>().ConvertUsing(converter);
        }
    }
}