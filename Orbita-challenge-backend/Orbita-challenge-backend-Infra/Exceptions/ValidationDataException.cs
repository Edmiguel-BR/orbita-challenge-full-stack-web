using FluentValidation.Results;
using Newtonsoft.Json;

namespace Orbita_challenge_backend_Infra.Exceptions
{
    public class ValidationDataException : Exception
    {
        public ValidationDataException(IEnumerable<ValidationFailure> validationFailures) : base(BuildMessage(validationFailures))
        {
        }

        public ValidationDataException(string message) : base(BuildMessage(message))
        {
        }

        private static string BuildMessage(IEnumerable<ValidationFailure> validationFailures)
        {
            var errors = (from failure in validationFailures
                          select new ValidationDataExceptionMessage() { Field = failure.PropertyName, ErrorMessage = failure.ErrorMessage }).ToList();

            return JsonConvert.SerializeObject(errors);
        }

        private static string BuildMessage(string message)
        {
            var errors = new List<ValidationDataExceptionMessage>();
            errors.Add(new ValidationDataExceptionMessage() { Field = string.Empty, ErrorMessage = message });

            return JsonConvert.SerializeObject(errors);
        }
    }

}