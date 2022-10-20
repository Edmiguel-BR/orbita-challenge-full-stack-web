using FluentValidation.Results;

namespace Orbita_challenge_backend_domain.Entities
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string RA { get; set; }
        public string CPF { get; set; }

        public void Validate(out ValidationResult validationResult)
        {
            validationResult = new AlunoValidator().Validate(this);
        }
    }
}
