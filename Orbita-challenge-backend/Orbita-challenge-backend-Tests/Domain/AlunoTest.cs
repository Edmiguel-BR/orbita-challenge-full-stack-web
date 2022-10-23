using Orbita_challenge_backend_domain.Entities;

namespace Orbita_challenge_backend_Tests.Domain
{
    public class AlunoTest
    {
        [Fact]
        public void Should_Create_A_Aluno_With_Valid_Data_And_Must_Be_Validated_Correctly()
        {
            var aluno = new Aluno()
            {
                Nome = "Fulano de tal",
                Email = "email@provedor.com",
                RA = "1234567890-ABC",
                CPF = "633.862.430-45"
            };

            aluno.Validate(out var validate);

            Assert.True(validate.IsValid);
            Assert.NotNull(validate);
            Assert.False(validate.Errors.Any());
        }

        [Theory]
        [InlineData("", "", "", "", "O CPF do aluno deve ser preenchido")]
        [InlineData("", "email.provedor.com", "", "63386243045", "O CPF do aluno deve ser preenchido corretamente")]
        [InlineData("", "email", "", "123456", "O CPF do aluno deve ser preenchido corretamente")]
        [InlineData("", "email", "", "123.123.123-12", "Informe um CPF válido para o aluno")]
        [InlineData("", "email", "", "123.123.123-1A", "O CPF do aluno deve ser preenchido corretamente")]
        public void Should_Create_A_Aluno_With_Invalid_Data_And_Must_Be_Invalidated(string nome, string email, string ra, string cpf, string cpfErrorMessage)
        {
            var aluno = new Aluno() { Nome = nome, Email = email, RA = ra, CPF = cpf };

            aluno.Validate(out var validate);

            Assert.False(validate.IsValid);
            Assert.NotNull(validate);
            Assert.True(validate.Errors.Any());
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "O nome do aluno deve ser preenchido");
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "Informe um e-mail válido para o aluno");
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "O registro acadêmico do aluno deve ser preenchido");
            Assert.Contains(validate.Errors, x => x.ErrorMessage == cpfErrorMessage);
        }

        [Fact]
        public void Should_Create_A_Aluno_With_Invalid_Data_Size_And_Must_Be_Invalidated()
        {
            var aluno = new Aluno()
            {
                Nome = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                Email = "email@provedor.com.bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbr",
                RA = "123456789-0123456789-0123456789-0123456789-0123456789-0",
                CPF = "111.111.111-11"
            };

            aluno.Validate(out var validate);

            Assert.False(validate.IsValid);
            Assert.NotNull(validate);
            Assert.True(validate.Errors.Any());
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "O tamanho máximo para o nome do aluno é de 100 caracteres");
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "O tamanho máximo para o e-mail do aluno é de 100 caracteres");
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "O tamanho máximo para o registro acadêmico do aluno é de 50 caracteres");
            Assert.Contains(validate.Errors, x => x.ErrorMessage == "Informe um CPF válido para o aluno");
        }
    }
}
