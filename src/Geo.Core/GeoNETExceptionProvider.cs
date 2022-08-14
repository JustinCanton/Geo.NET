// <copyright file="GeoNETExceptionProvider.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Geo.Core.Models.Exceptions;

    /// <summary>
    /// Used to provide an exception based on the exception type.
    /// </summary>
    public sealed class GeoNETExceptionProvider : IGeoNETExceptionProvider
    {
        private static readonly ConcurrentDictionary<Type, Func<string, Exception, Exception>> _cachedInnerExceptionDelegates = new ConcurrentDictionary<Type, Func<string, Exception, Exception>>();
        private static readonly ConcurrentDictionary<Type, Func<string, Exception>> _cachedExceptionDelegates = new ConcurrentDictionary<Type, Func<string, Exception>>();

        /// <inheritdoc/>
        public TException GetException<TException>(string message, Exception innerException = null)
            where TException : GeoCoreException
        {
            if (innerException == null)
            {
                var exceptionConstructor = GetExceptionConstructor(typeof(TException));
                return exceptionConstructor(message) as TException;
            }
            else
            {
                var exceptionConstructor = GetInnerExceptionConstructor(typeof(TException));
                return exceptionConstructor(message, innerException) as TException;
            }
        }

        private static Func<string, Exception, Exception> GetInnerExceptionConstructor(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_cachedInnerExceptionDelegates.TryGetValue(type, out var cachedConstructor))
            {
                return cachedConstructor;
            }

            var function = GetInnerExceptionDelegate(type);

            return _cachedInnerExceptionDelegates.AddOrUpdate(type, function, (key, value) => function);
        }

        private static Func<string, Exception> GetExceptionConstructor(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_cachedExceptionDelegates.TryGetValue(type, out var cachedConstructor))
            {
                return cachedConstructor;
            }

            var function = GetExceptionDelegate(type);

            return _cachedExceptionDelegates.AddOrUpdate(type, function, (key, value) => function);
        }

        private static Func<string, Exception, Exception> GetInnerExceptionDelegate(Type type)
        {
            var constructorParameters = new Type[] { typeof(string), typeof(Exception) };
            var constructor = type.GetConstructor(constructorParameters);
            var parameters = new List<ParameterExpression>()
            {
                Expression.Parameter(typeof(string)),
                Expression.Parameter(typeof(Exception)),
            };

            var expression = Expression.New(constructor, parameters);
            var lambdaExpression = Expression.Lambda<Func<string, Exception, Exception>>(expression, parameters);
            return lambdaExpression.Compile();
        }

        private static Func<string, Exception> GetExceptionDelegate(Type type)
        {
            var constructorParameters = new Type[] { typeof(string) };
            var constructor = type.GetConstructor(constructorParameters);
            var parameters = new List<ParameterExpression>()
            {
                Expression.Parameter(typeof(string)),
            };

            var expression = Expression.New(constructor, parameters);
            var lambdaExpression = Expression.Lambda<Func<string, Exception>>(expression, parameters);
            return lambdaExpression.Compile();
        }
    }
}
