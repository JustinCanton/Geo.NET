// <copyright file="ContextObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Tests.Models
{
    using System.Collections.Generic;
    using Geo.MapBox.Models.Responses;

    public class ContextObject
    {
        public IList<Context> Contexts { get; set; } = new List<Context>();
    }
}
