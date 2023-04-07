// <copyright file="GlobalSuppressions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Using Microsoft recommended unit test naming", Scope = "namespaceanddescendants", Target = "~N:Geo.ArcGIS.Tests")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "The name of the test should explain the test", Scope = "namespaceanddescendants", Target = "~N:Geo.ArcGIS.Tests")]
