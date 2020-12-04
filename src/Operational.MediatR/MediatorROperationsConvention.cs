using System;
using System.Xml;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.Conventions.FluentValidation;
using Rocket.Surgery.Conventions.MediatR;
using Rocket.Surgery.Conventions.AutoMapper;
using Rocket.Surgery.Operational.MediatR;

[assembly: Convention(typeof(MediatorROperationsConvention))]

namespace Rocket.Surgery.Operational.MediatR
{
    /// <summary>
    /// ValidationConvention.
    /// Implements the <see cref="IServiceConvention" />
    /// </summary>
    /// <seealso cref="IServiceConvention" />
    /// <seealso cref="IServiceConvention" />
    [PublicAPI]
    [DependsOnConvention(typeof(AutoMapperConvention))]
    [DependsOnConvention(typeof(MediatRConvention))]
    [DependsOnConvention(typeof(FluentValidationConvention))]
    public class MediatorROperationsConvention : IServiceConvention
    {
        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        public void Register(IConventionContext context, IConfiguration configuration, IServiceCollection services)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var serviceConfig = context.GetOrAdd(() => new MediatRServiceConfiguration());
            services.TryAddEnumerable(
                new ServiceDescriptor(
                    typeof(IPipelineBehavior<,>),
                    typeof(FluentValidationMediatRPipelineBehavior<,>),
                    serviceConfig.Lifetime
                )
            );
        }
    }
}