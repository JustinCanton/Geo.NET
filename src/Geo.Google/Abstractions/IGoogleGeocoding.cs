// <copyright file="IGoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Google.Models.Exceptions;
    using Geo.Google.Models.Parameters;
    using Geo.Google.Models.Responses;

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
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<GeocodingResponse> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<GeocodingResponse> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google find places API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="FindPlacesParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FindPlaceResponse"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<FindPlaceResponse> FindPlacesAsync(FindPlacesParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google nearby search API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="NearbySearchParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="PlaceResponse"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<PlaceResponse> NearbySearchAsync(NearbySearchParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google text search API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="TextSearchParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="PlaceResponse"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<PlaceResponse> TextSearchAsync(TextSearchParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google place details API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="DetailsParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="DetailsResponse"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<DetailsResponse> DetailsAsync(DetailsParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google place autocomplete API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="PlacesAutocompleteParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="AutocompleteResponse{PlaceAutocomplete}"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<AutocompleteResponse<PlaceAutocomplete>> PlaceAutocompleteAsync(PlacesAutocompleteParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google query autocomplete API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="QueryAutocompleteParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="AutocompleteResponse{QueryAutocomplete}"/> with the response from Google.</returns>
        /// <exception cref="GoogleException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<AutocompleteResponse<QueryAutocomplete>> QueryAutocompleteAsync(QueryAutocompleteParameters parameters, CancellationToken cancellationToken = default);
    }
}
