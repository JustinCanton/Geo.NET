// <copyright file="ContextObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Tests.Models
{
    using System.Collections.Generic;
    using Geo.MapBox.Models.Responses;

    public class ContextObject
    {
        public List<Context> Contexts { get; set; } = new List<Context>();
    }
}
