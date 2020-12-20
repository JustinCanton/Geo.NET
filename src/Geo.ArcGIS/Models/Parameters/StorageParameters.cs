// <copyright file="StorageParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    /// <summary>
    /// A parameters object for the storage flag information in ArcGIS request.
    /// </summary>
    public class StorageParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether the results of the operation will be persisted.
        /// The default value is false, which indicates the results of the operation can't be stored,
        /// but they can be temporarily displayed on a map for instance.
        /// If you store the results, in a database, for example, you need to set this parameter to true.
        ///
        /// Applications are contractually prohibited from storing the results of geocoding transactions unless
        /// they make the request by passing the forStorage parameter with a value of true and
        /// the token parameter with a valid ArcGIS Online token.
        /// Instructions for composing a request with a valid token are provided in the authentication topic.
        ///
        /// ArcGIS Online service credits are deducted from the organization account for each geocode
        /// transaction that includes the forStorage parameter with a value of true and a valid token.
        /// Refer to the ArcGIS Online service credits overview page
        /// (https://www.esri.com/en-us/arcgis/products/credits/overview?rmedium=esri_com_redirects01&rsource=https://www.esri.com/en-us/arcgis/products/arcgis-online/pricing/credits)
        /// for more information on how credits are charged.
        ///
        /// To learn more about free and paid geocoding operations, see
        /// (https://developers.arcgis.com/rest/geocode/api-reference/geocoding-free-vs-paid.htm).
        /// </summary>
        public bool ForStorage { get; set; } = false;
    }
}
