// <copyright file="PolylineEncoderDecoder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Geo.Here.Models.Parameters;

    /// <summary>
    /// A polyline encoder/decoder.
    /// </summary>
    internal class PolylineEncoderDecoder
    {
        /// <summary>
        /// Header version. A change in the version may affect the logic to encode and decode the rest of the header and data.
        /// </summary>
        private const byte FormatVersion = 1;

        /// <summary>
        /// Encode the list of coordinate triples.
        /// The third dimension value will be eligible for encoding only when ThirdDimension is other than ABSENT.
        /// This is lossy compression based on precision accuracy.
        /// </summary>
        /// <param name="coordinates">A <see cref="IEnumerable{T}"/> of <see cref="LatLngZ"/> of coordinate triples that to be encoded.</param>
        /// <param name="precision">Floating point precision of the coordinate to be encoded.</param>
        /// <param name="thirdDimension">A <see cref="ThirdDimension"/> which may be a level, altitude, elevation or some other custom value.</param>
        /// <param name="thirdDimPrecision">Floating point precision for thirdDimension value.</param>
        /// <returns>URL-safe encoded <see cref="string"/> for the given coordinates.</returns>
        public static string Encode(IEnumerable<LatLngZ> coordinates, int precision, ThirdDimension thirdDimension, int thirdDimPrecision)
        {
            if (!(coordinates?.Any() ?? false))
            {
                throw new ArgumentException("Invalid coordinates!");
            }

            if (!Enum.IsDefined(typeof(ThirdDimension), thirdDimension))
            {
                throw new ArgumentException("Invalid thirdDimension");
            }

            var encoder = new Encoder(precision, thirdDimension, thirdDimPrecision);
            foreach (var coordinate in coordinates)
            {
                encoder.Add(coordinate);
            }

            return encoder.GetEncoded();
        }

        /// <summary>
        /// Decode the encoded input <see cref="string"/> to a <see cref="IReadOnlyList{T}"/> of <see cref="LatLngZ"/>.
        /// </summary>
        /// <param name="encoded">The encoded URL-safe encoded <see cref="string"/>.</param>
        /// <returns>A <see cref="IReadOnlyList{T}"/> of <see cref="LatLngZ"/> coordinate triples that are decoded from the input.</returns>
        public static IReadOnlyList<LatLngZ> Decode(string encoded)
        {
            if (string.IsNullOrEmpty(encoded?.Trim()))
            {
                throw new ArgumentException("Invalid argument!", nameof(encoded));
            }

            var result = new List<LatLngZ>();
            var decoder = new Decoder(encoded);

            var lat = 0.0;
            var lng = 0.0;
            var z = 0.0;

            while (decoder.DecodeOne(ref lat, ref lng, ref z))
            {
                result.Add(new LatLngZ(lat, lng, z));
                lat = 0;
                lng = 0;
                z = 0;
            }

            return result;
        }

        /// <summary>
        /// Gets the <see cref="ThirdDimension"/> from the encoded input.
        /// </summary>
        /// <param name="encoded">URL-safe encoded coordinate triples.</param>
        /// <returns>The <see cref="ThirdDimension"/> from the url encoded string.</returns>
        public static ThirdDimension GetThirdDimension(string encoded)
        {
            int index = 0;
            long header = 0;
            Decoder.DecodeHeaderFromString(encoded.ToCharArray(), ref index, ref header);
            return (ThirdDimension)((header >> 4) & 0b_111);
        }

        /// <summary>
        /// Gets the version of the formatting.
        /// </summary>
        /// <returns>A <see cref="byte"/> with the version.</returns>
        public static byte GetVersion()
        {
            return FormatVersion;
        }

        /// <summary>
        /// Stateful instance for encoding and decoding on a sequence of Coordinates part of an request.
        /// Instance should be specific to type of coordinates (e.g. Lat, Lng)
        /// so that specific type delta is computed for encoding.
        /// Lat0 Lng0 3rd0 (Lat1-Lat0) (Lng1-Lng0) (3rdDim1-3rdDim0).
        /// </summary>
        internal class Converter
        {
            /// <summary>
            /// Base64 URL-safe characters.
            /// </summary>
            private static readonly char[] EncodingTable =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".ToCharArray();

            private static readonly int[] DecodingTable =
            {
                62, -1, -1, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1, -1,
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
                22, 23, 24, 25, -1, -1, -1, -1, 63, -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
                36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
            };

            private long _multiplier = 0;
            private long _lastValue = 0;

            /// <summary>
            /// Initializes a new instance of the <see cref="Converter"/> class.
            /// </summary>
            /// <param name="precision">The precision of the converter.</param>
            public Converter(int precision)
            {
                SetPrecision(precision);
            }

            /// <summary>
            /// Encodes and unsigned variable.
            /// </summary>
            /// <param name="value">The value to encode.</param>
            /// <param name="result">The string builder to append the result to.</param>
            public static void EncodeUnsignedVarint(long value, StringBuilder result)
            {
                while (value > 0x1F)
                {
                    byte pos = (byte)((value & 0x1F) | 0x20);
                    result.Append(EncodingTable[pos]);
                    value >>= 5;
                }

                result.Append(EncodingTable[(byte)value]);
            }

            /// <summary>
            /// Decodes and unsigned variable.
            /// </summary>
            /// <param name="encoded">The encoded <see cref="char"/>[] to decode.</param>
            /// <param name="index">The index in the <paramref name="encoded"/>.</param>
            /// <param name="result">The resulting decoded integer.</param>
            /// <returns>A boolean indicating whether or not the decoding was successful.</returns>
            public static bool DecodeUnsignedVarint(
                char[] encoded,
                ref int index,
                ref long result)
            {
                short shift = 0;
                long delta = 0;

                while (index < encoded.Length)
                {
                    long value = DecodeChar(encoded[index]);
                    if (value < 0)
                    {
                        return false;
                    }

                    index++;
                    delta |= (value & 0x1F) << shift;
                    if ((value & 0x20) == 0)
                    {
                        result = delta;
                        return true;
                    }
                    else
                    {
                        shift += 5;
                    }
                }

                if (shift > 0)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Encodes a value to a string representation.
            /// </summary>
            /// <param name="value">The value to encode.</param>
            /// <param name="result">The <see cref="StringBuilder"/> with the appended encoded string.</param>
            public void EncodeValue(double value, StringBuilder result)
            {
                /*
                 * Round-half-up
                 * round(-1.4) --> -1
                 * round(-1.5) --> -2
                 * round(-2.5) --> -3
                 */
                long scaledValue = (long)Math.Round(Math.Abs(value * _multiplier), MidpointRounding.AwayFromZero) * Math.Sign(value);
                long delta = scaledValue - _lastValue;
                bool negative = delta < 0;

                _lastValue = scaledValue;

                // make room on lowest bit
                delta <<= 1;

                // invert bits if the value is negative
                if (negative)
                {
                    delta = ~delta;
                }

                EncodeUnsignedVarint(delta, result);
            }

            /// <summary>
            /// Decode single coordinate (say lat|lng|z) starting at index.
            /// </summary>
            /// <param name="encoded">The encoded <see cref="char"/>[] of characters to decode.</param>
            /// <param name="index">The index in the <see cref="char"/>[].</param>
            /// <param name="coordinate">The resulting coordinate.</param>
            /// <returns>A boolean indicating whether the value was decoded or not.</returns>
            public bool DecodeValue(
                char[] encoded,
                ref int index,
                ref double coordinate)
            {
                long delta = 0;
                if (!DecodeUnsignedVarint(encoded, ref index, ref delta))
                {
                    return false;
                }

                if ((delta & 1) != 0)
                {
                    delta = ~delta;
                }

                delta >>= 1;
                _lastValue += delta;
                coordinate = (double)_lastValue / _multiplier;
                return true;
            }

            /// <summary>
            /// Decode a single char to the corresponding value.
            /// </summary>
            /// <param name="charValue">The character value to decode.</param>
            /// <returns>An integer representation of the character.</returns>
            private static int DecodeChar(char charValue)
            {
                int pos = charValue - 45;
                if (pos < 0 || pos > 77)
                {
                    return -1;
                }

                return DecodingTable[pos];
            }

            private void SetPrecision(int precision)
            {
                _multiplier = (long)Math.Pow(10, precision);
            }
        }

        /// <summary>
        /// Internal class for configuration, validation and encoding for an input request.
        /// </summary>
        private class Encoder
        {
            private readonly StringBuilder _result;
            private readonly Converter _latConverter;
            private readonly Converter _lngConverter;
            private readonly Converter _zConverter;
            private readonly ThirdDimension _thirdDimension;

            public Encoder(int precision, ThirdDimension thirdDimension, int thirdDimPrecision)
            {
                _latConverter = new Converter(precision);
                _lngConverter = new Converter(precision);
                _zConverter = new Converter(thirdDimPrecision);
                _thirdDimension = thirdDimension;
                _result = new StringBuilder();
                EncodeHeader(precision, (int)_thirdDimension, thirdDimPrecision);
            }

            public void Add(LatLngZ tuple)
            {
                if (tuple == null)
                {
                    throw new ArgumentNullException(nameof(tuple), "Invalid LatLngZ tuple");
                }

                Add(tuple.Lat, tuple.Lng, tuple.Z);
            }

            public string GetEncoded()
            {
                return _result.ToString();
            }

            private void EncodeHeader(int precision, int thirdDimensionValue, int thirdDimPrecision)
            {
                // Encode the `precision`, `third_dim` and `third_dim_precision` into one encoded char
                if (precision < 0 || precision > 15)
                {
                    throw new ArgumentException("precision out of range");
                }

                if (thirdDimPrecision < 0 || thirdDimPrecision > 15)
                {
                    throw new ArgumentException("thirdDimPrecision out of range");
                }

                if (thirdDimensionValue < 0 || thirdDimensionValue > 7)
                {
                    throw new ArgumentException("thirdDimensionValue out of range");
                }

                long res = (thirdDimPrecision << 7) | (thirdDimensionValue << 4) | precision;
                Converter.EncodeUnsignedVarint(PolylineEncoderDecoder.FormatVersion, _result);
                Converter.EncodeUnsignedVarint(res, _result);
            }

            private void Add(double lat, double lng)
            {
                _latConverter.EncodeValue(lat, _result);
                _lngConverter.EncodeValue(lng, _result);
            }

            private void Add(double lat, double lng, double z)
            {
                Add(lat, lng);
                if (_thirdDimension != ThirdDimension.Absent)
                {
                    _zConverter.EncodeValue(z, _result);
                }
            }
        }

        /// <summary>
        /// Single instance for decoding an input request.
        /// </summary>
        private class Decoder
        {
            private readonly char[] _encoded;
            private readonly Converter _latConverter;
            private readonly Converter _lngConverter;
            private readonly Converter _zConverter;

            private int _index;
            private int _precision;
            private int _thirdDimPrecision;
            private ThirdDimension _thirdDimension;

            public Decoder(string encoded)
            {
                _encoded = encoded.ToCharArray();
                _index = 0;
                DecodeHeader();
                _latConverter = new Converter(_precision);
                _lngConverter = new Converter(_precision);
                _zConverter = new Converter(_thirdDimPrecision);
            }

            public static void DecodeHeaderFromString(char[] encoded, ref int index, ref long header)
            {
                long value = 0;

                // Decode the header version
                if (!Converter.DecodeUnsignedVarint(encoded, ref index, ref value))
                {
                    throw new ArgumentException("Invalid encoding");
                }

                if (value != FormatVersion)
                {
                    throw new ArgumentException("Invalid format version");
                }

                // Decode the polyline header
                if (!Converter.DecodeUnsignedVarint(encoded, ref index, ref value))
                {
                    throw new ArgumentException("Invalid encoding");
                }

                header = value;
            }

            public bool DecodeOne(
                ref double lat,
                ref double lng,
                ref double z)
            {
                if (_index == _encoded.Length)
                {
                    return false;
                }

                if (!_latConverter.DecodeValue(_encoded, ref _index, ref lat))
                {
                    throw new ArgumentException("Invalid encoding");
                }

                if (!_lngConverter.DecodeValue(_encoded, ref _index, ref lng))
                {
                    throw new ArgumentException("Invalid encoding");
                }

                if (HasThirdDimension())
                {
                    if (!_zConverter.DecodeValue(_encoded, ref _index, ref z))
                    {
                        throw new ArgumentException("Invalid encoding");
                    }
                }

                return true;
            }

            private bool HasThirdDimension()
            {
                return _thirdDimension != ThirdDimension.Absent;
            }

            private void DecodeHeader()
            {
                long header = 0;
                DecodeHeaderFromString(_encoded, ref _index, ref header);
                _precision = (int)(header & 0b_1111); // we pick the first 4 bits only
                header >>= 4;
                _thirdDimension = (ThirdDimension)(header & 0b_111); // we pick the first 3 bits only
                _thirdDimPrecision = (int)((header >> 3) & 0b_1111);
            }
        }
    }
}
