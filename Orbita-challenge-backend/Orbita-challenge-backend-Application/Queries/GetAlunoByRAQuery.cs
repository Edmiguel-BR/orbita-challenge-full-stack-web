using FluentValidation.Results;
using MediatR;
using Orbita_challenge_backend_Application.Commands.Validators;
using Orbita_challenge_backend_Application.Queries.ViewModel;

namespace Orbita_challenge_backend_Application.Queries
{
    public class GetAlunoByRAQuery : IRequest<AlunoViewModel>
    {
        public string RA { get; set; }

        public void Validate(out ValidationResult validationResult)
        {
            validationResult = new GetAlunoByRAQueryValidator().Validate(this);
        }
    }
}
