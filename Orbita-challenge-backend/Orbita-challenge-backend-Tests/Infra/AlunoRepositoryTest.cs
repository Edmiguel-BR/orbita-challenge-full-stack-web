using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_domain.Entities;
using Orbita_challenge_backend_domain.Interfaces;
using Orbita_challenge_backend_Infra.Context;
using Orbita_challenge_backend_Infra.Exceptions;
using Orbita_challenge_backend_Infra.Repositories;

namespace Orbita_challenge_backend_Tests.Infra
{
    public class AlunoRepositoryTest
    {
        private readonly IAlunoRepository _repository;

        public AlunoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                         .UseInMemoryDatabase(databaseName: "TestDatabase")
                         .Options;
            var context = new AppDbContext(options);

            _repository = new AlunoRepository(context);
        }

        [Fact]
        public void Should_Insert_Aluno()
        {
            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = "1234567891-ABC", CPF = "426.721.100-00" };
            _repository.Insert(aluno);

            var alunoDb = _repository.GetByRA(aluno.RA).Result;

            Assert.NotNull(alunoDb);
            Assert.Equal(aluno.Nome, alunoDb.Nome);
            Assert.Equal(aluno.Email, alunoDb.Email);
            Assert.Equal(aluno.CPF, alunoDb.CPF);
        }

        [Fact]
        public void Should_Throw_Exception_When_Insert_An_Existing_Aluno()
        {
            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = "1234567881-ABC", CPF = "426.721.100-00" };
            _repository.Insert(aluno);

            var aluno2 = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = "1234567881-ABC", CPF = "426.721.100-00" };

            var exception = Assert.ThrowsAsync<ValidationDataException>(() => _repository.Insert(aluno2)).Result;

            Assert.Equal($"[{{\"Field\":\"\",\"ErrorMessage\":\"Aluno já cadastrado: {aluno2.RA}\"}}]", exception.Message);
        }

        [Fact]
        public void Should_Update_Aluno()
        {
            const string RA = "1234567892-ABC";

            var aluno = new Aluno() { Nome = "Aluno 2", Email = "aluno2@email.com.br", RA = RA, CPF = "633.862.430-45" };
            _repository.Insert(aluno);

            aluno.Email = "aluno1@newemail.com.br";

            _repository.Update(aluno);
            var alunoDb = _repository.GetByRA(RA).Result;

            Assert.Equal("aluno1@newemail.com.br", alunoDb.Email);
        }

        [Fact]
        public void Should_Throw_Exception_When_Update_An_Unexisted_Aluno()
        {
            const string RA = "1234567822-ABC";

            var aluno = new Aluno() { Nome = "Aluno 2", Email = "aluno2@email.com.br", RA = RA, CPF = "633.862.430-45" };

            var exception = Assert.Throws<ValidationDataException>(() => _repository.Update(aluno));

            Assert.Equal($"[{{\"Field\":\"\",\"ErrorMessage\":\"Aluno não encontrado: {RA}\"}}]", exception.Message);
        }

        [Fact]
        public void Should_Delete_Aluno()
        {
            const string RA = "1234567893-ABC";

            var aluno = new Aluno() { Nome = "Aluno 3", Email = "aluno3@email.com.br", RA = RA, CPF = "633.862.430-45" };
            _repository.Insert(aluno);

            _repository.Delete(RA);

            var alunoDb = _repository.GetByRA(RA).Result;

            Assert.Null(alunoDb);
        }

        [Fact]
        public void Should_Throw_Exception_When_Delete_An_Unexisted_Aluno()
        {
            const string RA = "1234567823-ABC";

            var exception = Assert.Throws<ValidationDataException>(() => _repository.Delete(RA));

            Assert.Equal($"[{{\"Field\":\"\",\"ErrorMessage\":\"Aluno não encontrado: {RA}\"}}]", exception.Message);
        }

        [Fact]
        public void Should_Get_All_Aluno()
        {
            var aluno = new Aluno() { Nome = "Aluno 4", Email = "aluno4@email.com.br", RA = "1234567894-ABC", CPF = "633.862.430-45" };
            _repository.Insert(aluno);

            var alunos = _repository.GetAll().Result;

            Assert.True(alunos.Any());
            Assert.Contains(alunos, a => a.RA == aluno.RA);
        }

        [Fact]
        public void Should_Get_Aluno_By_CPF()
        {
            const string CPF = "421.416.580-20";

            var aluno = new Aluno() { Nome = "Aluno 5", Email = "aluno5@email.com.br", RA = "1234567895-ABC", CPF = CPF };
            _repository.Insert(aluno);

            var alunoDb = _repository.GetByCPF(CPF).Result;

            Assert.Equal(alunoDb.RA, aluno.RA);

        }
    }
}
