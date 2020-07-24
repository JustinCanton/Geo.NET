// <copyright file="IGoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Google.Models;

    /// <summary>
    /// An interface for calling the Google geocoding methods.
    /// </summary>
    public interface IGoogleGeocoding
    {
        /// <summary>
        /// Calls the Google geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from Google.</returns>
        Task<GeocodingResponse> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from Google.</returns>
        Task<GeocodingResponse> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
