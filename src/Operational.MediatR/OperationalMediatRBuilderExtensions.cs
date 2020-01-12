using System;
using JetBrains.Annotations;
using Rocket.Surgery.Operational.MediatR;

// ReSharper disable once CheckNamespace
namespace Rocket.Surgery.Conventions
{
    /// <summary>
    /// FluentValidationHostBuilderExtensions.
    /// </summary>
    [PublicAPI]
    public static class OperationalMediatRBuilderExtensions
    {
        /// <summary>
        /// Adds fluent validation.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>IConventionHostBuilder.</returns>
        public static IConventionHostBuilder UseMediatROperations(this IConventionHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseAutoMapper();
            builder.UseMediatR();
            builder.UseFluentValidation();
            builder.Scanner.PrependConvention<MediatorROperationsConvention>();
            return builder;
        }
    }
}