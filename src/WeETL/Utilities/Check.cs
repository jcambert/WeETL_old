﻿using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WeETL.Properties;
using CA = System.Diagnostics.CodeAnalysis;
#nullable enable
namespace WeETL.Utilities
{
    [DebuggerStepThrough]
    internal static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>([NoEnumeration, CA.AllowNull, CA.NotNull] T value, [InvokerParameterName][NotNull] string parameterName)
        {
#pragma warning disable IDE0041 // Use 'is null' check
            if (ReferenceEquals(value, null))
#pragma warning restore IDE0041 // Use 'is null' check
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static IReadOnlyList<T> NotEmpty<T>(
            [CA.NotNull] IReadOnlyList<T>? value, [InvokerParameterName][NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(AbstractionsStrings.CollectionArgumentIsEmpty(parameterName));
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty([CA.NotNull] string? value, [InvokerParameterName][NotNull] string parameterName)
        {
            if (value is null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }

            if (value.Trim().Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(AbstractionsStrings.ArgumentIsEmpty(parameterName));
            }

            return value;
        }

        public static string? NullButNotEmpty(string? value, [InvokerParameterName][NotNull] string parameterName)
        {
            if (!(value is null)
                && value.Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(AbstractionsStrings.ArgumentIsEmpty(parameterName));
            }

            return value;
        }

        public static IReadOnlyList<T> HasNoNulls<T>(
            [CA.NotNull] IReadOnlyList<T>? value, [InvokerParameterName][NotNull] string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.Any(e => e == null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }

        public static IReadOnlyList<string> HasNoEmptyElements(
            [CA.NotNull] IReadOnlyList<string>? value,
            [InvokerParameterName][NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Any(s => string.IsNullOrWhiteSpace(s)))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(AbstractionsStrings.CollectionArgumentHasEmptyElements(parameterName));
            }

            return value;
        }

        [Conditional("DEBUG")]
        public static void DebugAssert([CA.DoesNotReturnIf(false)] bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"Check.DebugAssert failed: {message}");
            }
        }
    }
}
