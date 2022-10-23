using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Queries;
using Orbita_challenge_backend_Application.Service;
using Orbita_challenge_backend_domain.Entities;
using Orbita_challenge_backend_Infra.Context;
using Orbita_challenge_backend_Infra.Exceptions;
using Orbita_challenge_backend_Infra.Repositories;

namespace Orbita_challenge_backend_Tests.Application
{
    public class AlunoServiceTest
    {
        private readonly AlunoService _service;
        private readonly AlunoRepository _repository;

        public AlunoServiceTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                         .UseInMemoryDatabase(databaseName: "TestDatabase")
                         .Options;
            var context = new AppDbContext(options);

            _repository = new AlunoRepository(context);

            _service = new AlunoService(_repository);
        }

        [Fact]
        public void Should_Insert_Aluno()
        {
            var command = new InsertAlunoCommand() { Nome = "Aluno", Email = "aluno@email.com.br", RA = "1234567801-ABC", CPF = "426.721.100-00" };

            var result = _service.Insert(command).Result;

            Assert.True(result);
        }

        [Fact]
        public void Should_Throw_Exception_When_Insert_An_Invalid_Aluno()
        {
            var command = new InsertAlunoCommand() { Nome = "", Email = "", RA = "", CPF = "" };

            var exception = Assert.ThrowsAsync<ValidationDataException>(() => _service.Insert(command)).Result;

            Assert.Contains("O nome do aluno deve ser preenchido", exception.Message);
            Assert.Contains("Informe um e-mail válido para o aluno", exception.Message);
            Assert.Contains("O registro acadêmico do aluno deve ser preenchido", exception.Message);
            Assert.Contains("O CPF do aluno deve ser preenchido", exception.Message);
        }

        [Fact]
        public void Should_Update_Aluno()
        {
            const string RA = "1234567802-ABC";

            var command = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = "426.721.100-00" };

            _repository.Insert(command).GetAwaiter();

            var commandUpdate = new UpdateAlunoCommand() { Nome = "Aluno 2", Email = "aluno2@newemail.com.br", RA = RA };

            var result = _service.Update(commandUpdate);

            Assert.True(result);
            var alunoDb = _repository.GetByRA(RA).Result;

            Assert.Equal(commandUpdate.Email, alunoDb.Email);
            Assert.Equal(commandUpdate.Nome, alunoDb.Nome);
        }

        [Fact]
        public void Should_Throw_Exception_When_Update_An_Invalid_Aluno()
        {
            var command = new UpdateAlunoCommand() { Nome = "", Email = "", RA = "" };

            var exception = Assert.Throws<ValidationDataException>(() => _service.Update(command));

            Assert.Contains("O nome do aluno deve ser preenchido", exception.Message);
            Assert.Contains("Informe um e-mail válido para o aluno", exception.Message);
            Assert.Contains("O registro acadêmico do aluno deve ser preenchido", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Update_An_Unexisted_Aluno()
        {
            const string RA = "1234567822-ABC";

            var command = new UpdateAlunoCommand() { Nome = "Aluno 2", Email = "aluno2@email.com.br", RA = RA };

            var exception = Assert.Throws<ValidationDataException>(() => _service.Update(command));

            Assert.Equal($"[{{\"Field\":\"\",\"ErrorMessage\":\"Aluno não encontrado: {RA}\"}}]", exception.Message);
        }

        [Fact]
        public void Should_Delete_Aluno()
        {
            const string RA = "1234567803-ABC";

            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = "426.721.100-00" };

            _repository.Insert(aluno).GetAwaiter();

            var command = new DeleteAlunoCommand() { RA = RA };

            var result = _service.Delete(command);
            var alunoDb = _repository.GetByRA(RA).Result;

            Assert.True(result);
            Assert.Null(alunoDb);
        }


        [Fact]
        public void Should_Throw_Exception_When_Delete_An_Invalid_Aluno()
        {
            var command = new DeleteAlunoCommand() { RA = "" };

            var exception = Assert.Throws<ValidationDataException>(() => _service.Delete(command));

            Assert.Contains("O registro acadêmico do aluno deve ser preenchido", exception.Message);
        }

        [Fact]
        public void Should_Query_All_Alunos()
        {
            const string RA = "1234567803-ABC";

            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = "426.721.100-00" };

            _repository.Insert(aluno).GetAwaiter();

            var result = _service.GetAll().Result;

            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.True(result.First(x => x.RA == RA) != null);
        }


        [Fact]
        public void Should_Query_Aluno_By_CPF()
        {
            const string RA = "1234567804-ABC";
            const string CPF = "426.721.100-00";

            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = CPF };

            _repository.Insert(aluno).GetAwaiter();

            var command = new GetAlunoByCpfQuery() { CPF = CPF };

            var result = _service.GetByCpf(command).Result;

            Assert.NotNull(result);
            Assert.Equal(CPF, result.CPF);
        }

        [Fact]
        public void Should_Throw_Exception_When_Query_An_Invalid_Aluno_By_CPF()
        {
            var command = new GetAlunoByCpfQuery() { CPF = "" };

            var exception = Assert.ThrowsAsync<ValidationDataException>(() => _service.GetByCpf(command)).Result;

            Assert.Contains("O CPF do aluno deve ser preenchido", exception.Message);
        }


        [Fact]
        public void Should_Query_Aluno_By_RA()
        {
            const string RA = "1234567805-ABC";

            var aluno = new Aluno() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = "426.721.100-00" };

            _repository.Insert(aluno).GetAwaiter();

            var command = new GetAlunoByRAQuery() { RA = RA };

            var result = _service.GetByRA(command).Result;

            Assert.NotNull(result);
            Assert.Equal(RA, result.RA);
        }

        [Fact]
        public void Should_Throw_Exception_When_Query_An_Invalid_Aluno_By_RA()
        {
            var command = new GetAlunoByRAQuery() { RA = "" };

            var exception = Assert.ThrowsAsync<ValidationDataException>(() => _service.GetByRA(command)).Result;

            Assert.Contains("O registro acadêmico do aluno deve ser preenchido", exception.Message);
        }

    }
}
