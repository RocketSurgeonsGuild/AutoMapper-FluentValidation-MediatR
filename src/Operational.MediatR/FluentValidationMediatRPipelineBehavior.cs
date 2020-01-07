using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Rocket.Surgery.Operational.MediatR
{
    internal class FluentValidationMediatRPipelineBehavior<T, R> : IPipelineBehavior<T, R>
    {
        private readonly IValidatorFactory _validatorFactory;

        public FluentValidationMediatRPipelineBehavior(IValidatorFactory validatorFactory)
            => _validatorFactory = validatorFactory;

        public async Task<R> Handle(T request, CancellationToken cancellationToken, RequestHandlerDelegate<R> next)
        {
            var validator = _validatorFactory.GetValidator(typeof(T));
            if (validator != null)
            {
                var response = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
                if (!response.IsValid)
                {
                    throw new ValidationException(response.Errors);
                }
            }

            return await next().ConfigureAwait(false);
        }
    }
}