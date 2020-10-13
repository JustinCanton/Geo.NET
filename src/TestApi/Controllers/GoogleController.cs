// <copyright file="GoogleController.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace TestApi.Controllers
{
    using System.Threading.Tasks;
    using Geo.Google.Abstractions;
    using Geo.Google.Models.Parameters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class GoogleController : ControllerBase
    {
        private readonly IGoogleGeocoding _googleGeocoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleController"/> class.
        /// </summary>
        /// <param name="googleGeocoding">A <see cref="IGoogleGeocoding"/> used for google geocoding.</param>
        public GoogleController(IGoogleGeocoding googleGeocoding)
        {
            _googleGeocoding = googleGeocoding;
        }

        [HttpGet("geocoding")]
        public async Task<IActionResult> GetGeocodingResults([FromQuery]GeocodingParameters parameters)
        {
            var results = await _googleGeocoding.GeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet("reverse-geocoding")]
        public async Task<IActionResult> GetReverseGeocodingResults([FromQuery] ReverseGeocodingParameters parameters)
        {
            var results = await _googleGeocoding.ReverseGeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }
    }
}
