﻿using FluentValidation;

namespace Orbita_challenge_backend_Application.Commands.Validators
{
    public class DeleteAlunoCommandValidator : AbstractValidator<DeleteAlunoCommand>
    {
        public DeleteAlunoCommandValidator()
        {
            RuleFor(x => x.RA)
                 .NotEmpty()
                 .WithMessage("O registro acadêmico do aluno deve ser preenchido")
                 .MaximumLength(50)
                 .WithMessage("O tamanho máximo para o registro acadêmico do aluno é de 50 caracteres");
        }
    }
}