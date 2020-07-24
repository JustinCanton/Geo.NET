// <copyright file="IBingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Bing.Models;

    /// <summary>
    /// An interface for calling the Bing geocoding methods.
    /// </summary>
    public interface IBingGeocoding
    {
        /// <summary>
        /// Calls the Bing geocoding api and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        Task<Response> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
