// <copyright file="Attribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Geo.ArcGIS.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// A base attribute class.
    /// </summary>
    [JsonConverter(typeof(AttributeConverter))]
    public class Attribute
    {
    }
}
