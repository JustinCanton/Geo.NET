// <copyright file="FeatureType.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Enums
{
    /// <summary>
    /// Possible features of a result.
    /// </summary>
    public enum FeatureType
    {
        /// <summary>
        /// The result must be a country type.
        /// </summary>
        Country,

        /// <summary>
        /// The result must be a region type.
        /// </summary>
        Region,

        /// <summary>
        /// The result must be a postcode type.
        /// </summary>
        Postcode,

        /// <summary>
        /// The result must be a district type.
        /// </summary>
        District,

        /// <summary>
        /// The result must be a place type.
        /// </summary>
        Place,

        /// <summary>
        /// The result must be a locality type.
        /// </summary>
        Locality,

        /// <summary>
        /// The result must be a neighbourhood type.
        /// </summary>
        Neighborhood,

        /// <summary>
        /// The result must be an address type.
        /// </summary>
        Address,

        /// <summary>
        /// The result must be a point of interest type.
        /// </summary>
        POI,
    }
}
