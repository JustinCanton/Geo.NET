// <copyright file="IOlaGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;
    using Geo.Ola.Models.Parameters;
    using Geo.Ola.Models.Responses;

    /// <summary>
    /// An interface for calling the Ola geocoding methods.
    /// </summary>
    public interface IOlaGeocoding
    {
        /// <summary>
        /// Calls the Ola geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from Ola.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<GeocodingResponse> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Ola reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="ReverseGeocodingResponse"/> with the response from Ola.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<ReverseGeocodingResponse> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
