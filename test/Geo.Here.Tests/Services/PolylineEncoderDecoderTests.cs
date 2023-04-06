// <copyright file="PolylineEncoderDecoderTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using FluentAssertions;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Services;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="PolylineEncoderDecoder"/> class.
    /// </summary>
    public class PolylineEncoderDecoderTests
    {
        private static readonly Regex EncodedResultRegex = new Regex(@"^\{\((?:(?<prec>\d+)(?:, ?)?)+\); \[(?<latlngz>\((?:(?:-?\d+\.\d+)(?:, ?)?){2,3}\)(?:, ?)?)+\]\}", RegexOptions.Compiled);
        private static readonly Regex EncodedResultCoordinatesRegex = new Regex(@"^\((?:(?<number>-?\d+\.\d+)(?:, ?)?){2,3}\)(?:, ?)?", RegexOptions.Compiled);

        [Fact]
        public void EncodedFlexilineMatchesDecodedResultTest()
        {
            using (var decodedEnumerator = File.ReadLines("TestData/RoundHalfUp/decoded.txt").GetEnumerator())
            {
                foreach (var flexiline in File.ReadLines("TestData/RoundHalfUp/encoded.txt"))
                {
                    decodedEnumerator.MoveNext();
                    var testResultStr = decodedEnumerator.Current;

                    var expectedResult = ParseExpectedTestResult(testResultStr);

                    var decoded = PolylineEncoderDecoder.Decode(flexiline);
                    var encoded = PolylineEncoderDecoder.Encode(
                        decoded,
                        expectedResult.Precision.Precision2d,
                        expectedResult.Precision.Type3d,
                        expectedResult.Precision.Precision3d);

                    ThirdDimension thirdDimension = PolylineEncoderDecoder.GetThirdDimension(encoded);
                    expectedResult.Precision.Type3d.Should().Be(thirdDimension);

                    for (int i = 0; i < decoded.Count; i++)
                    {
                        AssertEqualWithPrecision(
                            expectedResult.Coordinates[i].Latitude,
                            decoded[i].Latitude,
                            expectedResult.Precision.Precision2d);

                        AssertEqualWithPrecision(
                            expectedResult.Coordinates[i].Longitude,
                            decoded[i].Longitude,
                            expectedResult.Precision.Precision2d);

                        AssertEqualWithPrecision(
                            expectedResult.Coordinates[i].Z,
                            decoded[i].Z,
                            expectedResult.Precision.Precision3d);
                    }

                    if (flexiline != encoded)
                    {
                        Console.WriteLine($@"WARNING expected {flexiline} but got {encoded}");
                    }
                }
            }
        }

        [Fact]
        public void TestInvalidCoordinates()
        {
            // Arrange
            // Null coordinates
            Action act1 = () => PolylineEncoderDecoder.Encode(null, 5, ThirdDimension.Absent, 0);

            // Empty coordinates list test
            Action act2 = () => PolylineEncoderDecoder.Encode(new List<LatLngZ>(), 5, ThirdDimension.Absent, 0);

            // Act/Assert
            act1.Should().Throw<ArgumentException>();
            act2.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TestInvalidThirdDimension()
        {
            // Arrange
            var pairs = new List<LatLngZ>();
            pairs.Add(new LatLngZ(50.1022829, 8.6982122));
            ThirdDimension invalid = (ThirdDimension)999;

            Action act = () => PolylineEncoderDecoder.Encode(pairs, 5, invalid, 0);

            // Act/Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TestConvertValue()
        {
            PolylineEncoderDecoder.Converter conv = new PolylineEncoderDecoder.Converter(5);
            StringBuilder result = new StringBuilder();
            conv.EncodeValue(-179.98321, result);
            result.ToString().Should().Be("h_wqiB");
        }

        [Fact]
        public void TestSimpleLatLngEncoding()
        {
            var pairs = new List<LatLngZ>();
            pairs.Add(new LatLngZ(50.1022829, 8.6982122));
            pairs.Add(new LatLngZ(50.1020076, 8.6956695));
            pairs.Add(new LatLngZ(50.1006313, 8.6914960));
            pairs.Add(new LatLngZ(50.0987800, 8.6875156));

            var expected = "BFoz5xJ67i1B1B7PzIhaxL7Y";
            var computed = PolylineEncoderDecoder.Encode(pairs, 5, ThirdDimension.Absent, 0);
            computed.Should().Be(expected);
        }

        [Fact]
        public void TestComplexLatLngEncoding()
        {
            var pairs = new List<LatLngZ>();
            pairs.Add(new LatLngZ(52.5199356, 13.3866272));
            pairs.Add(new LatLngZ(52.5100899, 13.2816896));
            pairs.Add(new LatLngZ(52.4351807, 13.1935196));
            pairs.Add(new LatLngZ(52.4107285, 13.1964502));
            pairs.Add(new LatLngZ(52.38871, 13.1557798));
            pairs.Add(new LatLngZ(52.3727798, 13.1491003));
            pairs.Add(new LatLngZ(52.3737488, 13.1154604));
            pairs.Add(new LatLngZ(52.3875198, 13.0872202));
            pairs.Add(new LatLngZ(52.4029388, 13.0706196));
            pairs.Add(new LatLngZ(52.4105797, 13.0755529));

            var expected = "BF05xgKuy2xCx9B7vUl0OhnR54EqSzpEl-HxjD3pBiGnyGi2CvwFsgD3nD4vB6e";
            var computed = PolylineEncoderDecoder.Encode(pairs, 5, ThirdDimension.Absent, 0);
            computed.Should().Be(expected);
        }

        [Fact]
        public void TestLatLngZEncode()
        {
            var tuples = new List<LatLngZ>();
            tuples.Add(new LatLngZ(50.1022829, 8.6982122, 10));
            tuples.Add(new LatLngZ(50.1020076, 8.6956695, 20));
            tuples.Add(new LatLngZ(50.1006313, 8.6914960, 30));
            tuples.Add(new LatLngZ(50.0987800, 8.6875156, 40));

            var expected = "BlBoz5xJ67i1BU1B7PUzIhaUxL7YU";
            var computed = PolylineEncoderDecoder.Encode(tuples, 5, ThirdDimension.Altitude, 0);
            computed.Should().Be(expected);
        }

        /**********************************************/
        /********** Decoder test starts ***************/
        /**********************************************/
        [Fact]
        public void TestInvalidEncoderInput()
        {
            // Arrange
            // Null coordinates
            Action act1 = () => PolylineEncoderDecoder.Decode(null);

            // Empty coordinates list test
            Action act2 = () => PolylineEncoderDecoder.Decode(string.Empty);

            // Act/Assert
            act1.Should().Throw<ArgumentException>();
            act2.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TestThirdDimension()
        {
            PolylineEncoderDecoder.GetThirdDimension("BFoz5xJ67i1BU").Should().Be(ThirdDimension.Absent);
            PolylineEncoderDecoder.GetThirdDimension("BVoz5xJ67i1BU").Should().Be(ThirdDimension.Level);
            PolylineEncoderDecoder.GetThirdDimension("BlBoz5xJ67i1BU").Should().Be(ThirdDimension.Altitude);
            PolylineEncoderDecoder.GetThirdDimension("B1Boz5xJ67i1BU").Should().Be(ThirdDimension.Elevation);
        }

        [Fact]
        public void TestDecodeConvertValue()
        {
            var encoded = "h_wqiB";
            var expected = -179.98321;
            var index = 0;
            var computed = 0d;
            var conv = new PolylineEncoderDecoder.Converter(5);
            conv.DecodeValue(encoded.ToCharArray(), ref index, ref computed);
            computed.Should().Be(expected);
        }

        [Fact]
        public void TestSimpleLatLngDecoding()
        {
            var computed = PolylineEncoderDecoder.Decode("BFoz5xJ67i1B1B7PzIhaxL7Y");
            var expected = new List<LatLngZ>();
            expected.Add(new LatLngZ(50.10228, 8.69821));
            expected.Add(new LatLngZ(50.10201, 8.69567));
            expected.Add(new LatLngZ(50.10063, 8.69150));
            expected.Add(new LatLngZ(50.09878, 8.68752));

            computed.Count.Should().Be(expected.Count);
            for (int i = 0; i < computed.Count; ++i)
            {
                computed[i].Should().Be(expected[i]);
            }
        }

        [Fact]
        public void TestComplexLatLngDecoding()
        {
            var computed = PolylineEncoderDecoder.Decode("BF05xgKuy2xCx9B7vUl0OhnR54EqSzpEl-HxjD3pBiGnyGi2CvwFsgD3nD4vB6e");

            var pairs = new List<LatLngZ>();
            pairs.Add(new LatLngZ(52.51994, 13.38663));
            pairs.Add(new LatLngZ(52.51009, 13.28169));
            pairs.Add(new LatLngZ(52.43518, 13.19352));
            pairs.Add(new LatLngZ(52.41073, 13.19645));
            pairs.Add(new LatLngZ(52.38871, 13.15578));
            pairs.Add(new LatLngZ(52.37278, 13.14910));
            pairs.Add(new LatLngZ(52.37375, 13.11546));
            pairs.Add(new LatLngZ(52.38752, 13.08722));
            pairs.Add(new LatLngZ(52.40294, 13.07062));
            pairs.Add(new LatLngZ(52.41058, 13.07555));

            computed.Count.Should().Be(pairs.Count);
            for (int i = 0; i < computed.Count; ++i)
            {
                computed[i].Should().Be(pairs[i]);
            }
        }

        [Fact]
        public void TestLatLngZDecode()
        {
            var computed = PolylineEncoderDecoder.Decode("BlBoz5xJ67i1BU1B7PUzIhaUxL7YU");
            var tuples = new List<LatLngZ>();

            tuples.Add(new LatLngZ(50.10228, 8.69821, 10));
            tuples.Add(new LatLngZ(50.10201, 8.69567, 20));
            tuples.Add(new LatLngZ(50.10063, 8.69150, 30));
            tuples.Add(new LatLngZ(50.09878, 8.68752, 40));

            computed.Count.Should().Be(tuples.Count);
            for (int i = 0; i < computed.Count; ++i)
            {
                computed[i].Should().Be(tuples[i]);
            }
        }

        private static void AssertEqualWithPrecision(double expected, double actual, int precision)
        {
            long expectedLong = (long)Math.Round(expected * (int)Math.Pow(10, precision), MidpointRounding.AwayFromZero);
            long actualLong = (long)Math.Round(actual * (int)Math.Pow(10, precision), MidpointRounding.AwayFromZero);

            expectedLong.Should().Be(actualLong);
        }

        private static ExpectedResult ParseExpectedTestResult(string encodedResult)
        {
            var resultMatch = EncodedResultRegex.Match(encodedResult);
            if (!resultMatch.Success)
            {
                throw new ArgumentException("encodedResult");
            }

            Precision precision;
            if (resultMatch.Groups["prec"].Captures.Count == 1)
            {
                precision = new Precision(int.Parse(resultMatch.Groups["prec"].Captures[0].Value, CultureInfo.InvariantCulture));
            }
            else if (resultMatch.Groups["prec"].Captures.Count == 3)
            {
                precision = new Precision(
                    int.Parse(resultMatch.Groups["prec"].Captures[0].Value, CultureInfo.InvariantCulture),
                    int.Parse(resultMatch.Groups["prec"].Captures[1].Value, CultureInfo.InvariantCulture),
                    (ThirdDimension)int.Parse(resultMatch.Groups["prec"].Captures[2].Value, CultureInfo.InvariantCulture));
            }
            else
            {
                throw new ArgumentException("encodedResult");
            }

            var coordinates = new List<LatLngZ>();
            foreach (Capture latlngzCapture in resultMatch.Groups["latlngz"].Captures)
            {
                LatLngZ coordinate;
                var coordinateMatch = EncodedResultCoordinatesRegex.Match(latlngzCapture.Value);
                if (coordinateMatch.Groups["number"].Captures.Count == 2)
                {
                    coordinate = new LatLngZ(
                        double.Parse(coordinateMatch.Groups["number"].Captures[0].Value, CultureInfo.InvariantCulture),
                        double.Parse(coordinateMatch.Groups["number"].Captures[1].Value, CultureInfo.InvariantCulture));
                }
                else if (coordinateMatch.Groups["number"].Captures.Count == 3)
                {
                    coordinate = new LatLngZ(
                        double.Parse(coordinateMatch.Groups["number"].Captures[0].Value, CultureInfo.InvariantCulture),
                        double.Parse(coordinateMatch.Groups["number"].Captures[1].Value, CultureInfo.InvariantCulture),
                        double.Parse(coordinateMatch.Groups["number"].Captures[2].Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    throw new ArgumentException("latlngz");
                }

                coordinates.Add(coordinate);
            }

            return new ExpectedResult(precision, coordinates);
        }

        private class Precision
        {
            public Precision(int precision2d, int precision3d = 0, ThirdDimension type3d = ThirdDimension.Absent)
            {
                Precision2d = precision2d;
                Precision3d = precision3d;
                Type3d = type3d;
            }

            public int Precision2d { get; }

            public int Precision3d { get; }

            public ThirdDimension Type3d { get; }
        }

        private class ExpectedResult
        {
            public ExpectedResult(Precision precision, List<LatLngZ> coordinates)
            {
                Precision = precision;
                Coordinates = coordinates;
            }

            public Precision Precision { get; }

            public List<LatLngZ> Coordinates { get; }
        }
    }
}
