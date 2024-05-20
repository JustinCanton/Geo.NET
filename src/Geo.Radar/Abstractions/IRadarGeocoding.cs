// <copyright file="IRadarGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;
    using Geo.Radar.Models.Parameters;
    using Geo.Radar.Models.Responses;

    /// <summary>
    /// An interface for calling the Radar geocoding methods.
    /// </summary>
    public interface IRadarGeocoding
    {
        /// <summary>
        /// Calls the Radar geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response{T}"/> of <see cref="GeocodeAddress"/> with the response from Radar.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response<GeocodeAddress>> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Radar reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response{T}"/> of <see cref="ReverseGeocodeAddress"/> with the response from Radar.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response<ReverseGeocodeAddress>> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Radar autocomplete API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="AutocompleteParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response{T}"/> of <see cref="ReverseGeocodeAddress"/> with the response from Radar.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response<ReverseGeocodeAddress>> AutocompleteAsync(AutocompleteParameters parameters, CancellationToken cancellationToken = default);
    }
}
