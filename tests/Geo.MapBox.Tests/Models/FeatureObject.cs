// <copyright file="FeatureObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Tests.Models
{
    using System.Collections.Generic;
    using Geo.MapBox.Models.Responses;

    public class FeatureObject
    {
        public List<Feature> Features { get; } = new List<Feature>();
    }
}
