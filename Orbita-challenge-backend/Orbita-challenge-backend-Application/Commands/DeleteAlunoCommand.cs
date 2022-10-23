using FluentValidation.Results;
using MediatR;
using Orbita_challenge_backend_Application.Commands.Validators;

namespace Orbita_challenge_backend_Application.Commands
{
    public class DeleteAlunoCommand : IRequest<bool>
    {
        public string RA { get; set; }

        public void Validate(out ValidationResult validationResult)
        {
            validationResult = new DeleteAlunoCommandValidator().Validate(this);
        }
    }
}
