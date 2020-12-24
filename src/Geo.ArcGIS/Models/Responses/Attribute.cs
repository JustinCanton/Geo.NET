// <copyright file="Attribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
