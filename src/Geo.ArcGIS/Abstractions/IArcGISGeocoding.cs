// <copyright file="IArcGISGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.ArcGIS.Models.Exceptions;
    using Geo.ArcGIS.Models.Parameters;
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// An interface for calling the ArcGIS geocoding API.
    /// </summary>
    public interface IArcGISGeocoding
    {
        /// <summary>
        /// Calls the ArcGIS address candidate API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressCandidateParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="CandidateResponse"/> with the response from ArcGIS.</returns>
        /// <exception cref="ArcGISException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<CandidateResponse> AddressCandidateAsync(AddressCandidateParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the ArcGIS place candidate API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="PlaceCandidateParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="CandidateResponse"/> with the response from ArcGIS.</returns>
        /// <exception cref="ArcGISException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<CandidateResponse> PlaceCandidateAsync(PlaceCandidateParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the ArcGIS suggestion API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="SuggestParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="SuggestResponse"/> with the response from ArcGIS.</returns>
        /// <exception cref="ArcGISException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<SuggestResponse> SuggestAsync(SuggestParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the ArcGIS reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="ReverseGeocodingResponse"/> with the response from ArcGIS.</returns>
        /// <exception cref="ArcGISException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<ReverseGeocodingResponse> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the ArcGIS geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from ArcGIS.</returns>
        /// <exception cref="ArcGISException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<GeocodingResponse> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
