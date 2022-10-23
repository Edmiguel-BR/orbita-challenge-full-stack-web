﻿using FluentValidation.Results;
using MediatR;
using Orbita_challenge_backend_Application.Commands.Validators;


namespace Orbita_challenge_backend_Application.Commands
{
    public class UpdateAlunoCommand : IRequest<bool>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string RA { get; set; }

        public void Validate(out ValidationResult validationResult)
        {
            validationResult = new UpdateAlunoCommandValidator().Validate(this);
        }

    }
}