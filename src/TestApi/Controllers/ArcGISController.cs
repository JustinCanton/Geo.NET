// <copyright file="ArcGISController.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace TestApi.Controllers
{
    using System.Threading.Tasks;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models.Parameters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ArcGISController : ControllerBase
    {
        private readonly IArcGISGeocoding _arcGISGeocoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISController"/> class.
        /// </summary>
        /// <param name="arcGISGeocoding">A <see cref="IArcGISGeocoding"/> used for ArcGIS geocoding.</param>
        public ArcGISController(IArcGISGeocoding arcGISGeocoding)
        {
            _arcGISGeocoding = arcGISGeocoding;
        }

        [HttpGet("address-candidate")]
        public async Task<IActionResult> GetAddressCandidateResults([FromQuery] AddressCandidateParameters parameters)
        {
            var results = await _arcGISGeocoding.AddressCandidateAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet("place-candidate")]
        public async Task<IActionResult> GetPlaceCandidateResults([FromQuery] PlaceCandidateParameters parameters)
        {
            var results = await _arcGISGeocoding.PlaceCandidateAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet("suggest")]
        public async Task<IActionResult> GetSuggestResults([FromQuery] SuggestParameters parameters)
        {
            var results = await _arcGISGeocoding.SuggestAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet("reverse-geocoding")]
        public async Task<IActionResult> GetReverseGeocodingResults([FromQuery] ReverseGeocodingParameters parameters)
        {
            var results = await _arcGISGeocoding.ReverseGeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }
    }
}
