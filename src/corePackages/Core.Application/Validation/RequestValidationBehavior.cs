﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Validation
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            ValidationContext<object> context = new(request);
            List<ValidationFailure> failures = _validators
                                              .Select(validator => validator.Validate(context))
                                              .SelectMany(result => result.Errors)
                                              .Where(failures => failures != null)
                                              .ToList();
            if (failures.Count != 0) throw new ValidationException(failures);
            return next();
        }
    }
}