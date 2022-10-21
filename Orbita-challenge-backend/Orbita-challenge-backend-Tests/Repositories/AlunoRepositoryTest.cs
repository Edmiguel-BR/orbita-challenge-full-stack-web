using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_domain.Entities;
using Orbita_challenge_backend_domain.Interfaces;
using Orbita_challenge_backend_Infra.Context;
using Orbita_challenge_backend_Infra.Repositories;

namespace Orbita_challenge_backend_Tests.Repositories
{

    public class AlunoRepositoryTest
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                         .UseInMemoryDatabase(databaseName: "TestDatabase")
                         .Options;
            var context = new AppDbContext(options);

            _alunoRepository = new AlunoRepository(context);
        }

        [Fact]
        public void Should_Insert_Aluno()
        {
            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = "1234567891-ABC", CPF = "426.721.100-00" };
            _alunoRepository.Insert(aluno);

            var alunoDb = _alunoRepository.GetByRA(aluno.RA).Result;

            Assert.NotNull(alunoDb);
            Assert.Equal(aluno.Nome, alunoDb.Nome);
            Assert.Equal(aluno.Email, alunoDb.Email);
            Assert.Equal(aluno.CPF, alunoDb.CPF);
        }

        [Fact]
        public void Should_Update_Aluno()
        {
            const string RA = "1234567892-ABC";
            var aluno = new Aluno() { Nome = "Aluno 2", Email = "aluno2@email.com.br", RA = RA, CPF = "633.862.430-45" };
            _alunoRepository.Insert(aluno);

            aluno.Email = "aluno1@newemail.com.br";

            _alunoRepository.Update(aluno);
            var alunoDb = _alunoRepository.GetByRA(RA).Result;

            Assert.Equal("aluno1@newemail.com.br", alunoDb.Email);
        }

        [Fact]
        public void Should_Delete_Aluno()
        {
            const string RA = "1234567893-ABC";
            var aluno = new Aluno() { Nome = "Aluno 3", Email = "aluno3@email.com.br", RA = RA, CPF = "633.862.430-45" };
            _alunoRepository.Insert(aluno);

            _alunoRepository.Delete(RA);
            var alunoDb = _alunoRepository.GetByRA("1234567891-ABC").Result;

            Assert.Null(alunoDb);
        }

        [Fact]
        public void Should_Get_All_Aluno()
        {
            var aluno = new Aluno() { Nome = "Aluno 4", Email = "aluno4@email.com.br", RA = "1234567894-ABC", CPF = "633.862.430-45" };
            _alunoRepository.Insert(aluno);

            var alunos = _alunoRepository.GetAll().Result;

            Assert.True(alunos.Any());
            Assert.Contains(alunos, a => a.RA == aluno.RA);
        }

        [Fact]
        public void Should_Get_Aluno_By_CPF()
        {
            const string CPF = "421.416.580-20";
            var aluno = new Aluno() { Nome = "Aluno 5", Email = "aluno5@email.com.br", RA = "1234567895-ABC", CPF = CPF };
            _alunoRepository.Insert(aluno);

            var alunoDb = _alunoRepository.GetByCPF(CPF).Result;

            Assert.Equal(alunoDb.RA, aluno.RA);

        }
    }
}
