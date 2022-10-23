using FluentValidation;

namespace Orbita_challenge_backend_Application.Commands.Validators
{
    public class UpdateAlunoCommandValidator : AbstractValidator<UpdateAlunoCommand>
    {
        public UpdateAlunoCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome do aluno deve ser preenchido")
                .MaximumLength(100)
                .WithMessage("O tamanho máximo para o nome do aluno é de 100 caracteres");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Informe um e-mail válido para o aluno")
                .MaximumLength(100)
                .WithMessage("O tamanho máximo para o e-mail do aluno é de 100 caracteres");

            RuleFor(x => x.RA)
                 .NotEmpty()
                 .WithMessage("O registro acadêmico do aluno deve ser preenchido")
                 .MaximumLength(50)
                 .WithMessage("O tamanho máximo para o registro acadêmico do aluno é de 50 caracteres");
        }
    }
}
