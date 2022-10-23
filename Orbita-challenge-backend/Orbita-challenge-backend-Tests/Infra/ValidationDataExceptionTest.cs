using FluentValidation.Results;
using Orbita_challenge_backend_Infra.Exceptions;

namespace Orbita_challenge_backend_Tests.Infra
{
    public class ValidationDataExceptionTest
    {
        [Fact]
        public void Should_Create_Exception_With_Text_Message()
        {
            var exception = new ValidationDataException("test");

            Assert.NotNull(exception);
            Assert.IsType<ValidationDataException>(exception);
            Assert.Equal("[{\"Field\":\"\",\"ErrorMessage\":\"test\"}]", exception.Message);
        }

        [Fact]
        public void Should_Create_Exception_With_Failure_List()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure() { PropertyName = "Field", ErrorMessage = "Message" }
            };

            var exception = new ValidationDataException(validationFailures);

            Assert.NotNull(exception);
            Assert.IsType<ValidationDataException>(exception);
            Assert.Equal("[{\"Field\":\"Field\",\"ErrorMessage\":\"Message\"}]", exception.Message);
        }
    }
}
