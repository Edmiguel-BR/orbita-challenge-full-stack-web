using FluentValidation;
using System.Text.RegularExpressions;

namespace Orbita_challenge_backend_domain.Entities
{
    internal class AlunoValidator : AbstractValidator<Aluno>
    {
        internal AlunoValidator()
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

            RuleFor(x => x.CPF)
                .Custom(CPFValidation());
        }

        private static Action<string, ValidationContext<Aluno>> CPFValidation() => (cpf, context) =>
        {
            if (cpf == string.Empty)
            {
                context.AddFailure("CPF", "O CPF do aluno deve ser preenchido");
            }
            else if (!Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$"))
            {
                context.AddFailure("CPF", "O CPF do aluno deve ser preenchido corretamente");
            }
            else if (!CpfValidate(cpf))
            {
                context.AddFailure("CPF", "Informe um CPF válido para o aluno");
            }
        };

        private static bool CpfValidate(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            var strCpf = cpf.ToString().PadLeft(11, '0');

            if (strCpf.All(x => x == strCpf[0]))
            {
                return false;
            }

            var listCpf = strCpf.Select(num => Convert.ToInt32(num.ToString())).ToList();

            return listCpf[9] == Mod11Cpf(listCpf, 10)
                && listCpf[10] == Mod11Cpf(listCpf, 11);
        }

        private static int Mod11Cpf(List<int> elements, int @base)
        {
            var sum = 0;

            for (var i = 0; i < (@base - 1); i++)
            {
                sum += (@base - i) * elements[i];
            }

            int dv1, leftover = sum % 11;

            dv1 = leftover < 2 ? 0 : 11 - leftover;

            return dv1;
        }
    }

}
