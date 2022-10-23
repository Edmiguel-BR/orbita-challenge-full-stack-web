using FluentValidation;
using Orbita_challenge_backend_Application.Queries;

namespace Orbita_challenge_backend_Application.Commands.Validators
{
    public class GetAlunoByCpfQueryValidator : AbstractValidator<GetAlunoByCpfQuery>
    {
        public GetAlunoByCpfQueryValidator()
        {
            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("O CPF do aluno deve ser preenchido")
                .Length(14)
                .WithMessage("O CPF deve ser preenchido com 14 caracteres: 999.999.999-99");
        }
    }
}
