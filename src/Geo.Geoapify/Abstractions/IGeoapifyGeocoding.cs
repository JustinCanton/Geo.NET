// <copyright file="IGeoapifyGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;
    using Geo.Geoapify.Models.Parameters;
    using Geo.Geoapify.Models.Responses;

    /// <summary>
    /// An interface for calling the Geoapify geocoding methods.
    /// </summary>
    public interface IGeoapifyGeocoding
    {
        /// <summary>
        /// Calls the Geoapify forward geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> with the response from Geoapify.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<FeatureCollection> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Geoapify reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> with the response from Geoapify.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<FeatureCollection> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Geoapify address autocomplete API and returns the results.
        /// </summary>
        /// <param name="parameters">An <see cref="AutocompleteParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> with the response from Geoapify.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<FeatureCollection> AutocompleteAsync(AutocompleteParameters parameters, CancellationToken cancellationToken = default);
    }
}
