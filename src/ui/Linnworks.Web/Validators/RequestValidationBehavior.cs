using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using ValidationException = Linnworks.Web.Exceptions.ValidationException;
using Microsoft.AspNetCore.Mvc;

namespace Linnworks.Web.Validators
{
    public class RequestValidationBehavior : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, IValidationContext commonContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return result;
        }

        public IValidationContext BeforeMvcValidation(ControllerContext controllerContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
