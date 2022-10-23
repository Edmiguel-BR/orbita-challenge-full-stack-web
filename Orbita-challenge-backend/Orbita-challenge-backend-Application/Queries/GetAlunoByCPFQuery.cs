using FluentValidation.Results;
using MediatR;
using Orbita_challenge_backend_Application.Commands.Validators;
using Orbita_challenge_backend_Application.Queries.ViewModel;

namespace Orbita_challenge_backend_Application.Queries
{
    public class GetAlunoByCpfQuery : IRequest<AlunoViewModel>
    {
        public string CPF { get; set; }

        public void Validate(out ValidationResult validationResult)
        {
            validationResult = new GetAlunoByCpfQueryValidator().Validate(this);
        }
    }
}
