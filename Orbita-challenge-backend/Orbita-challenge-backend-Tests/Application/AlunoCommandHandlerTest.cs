using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Handlers;
using Orbita_challenge_backend_Application.Service;
using Orbita_challenge_backend_Infra.Context;
using Orbita_challenge_backend_Infra.Repositories;

namespace Orbita_challenge_backend_Tests.Application
{
    public class AlunoCommandHandlerTest
    {
        private readonly AlunoCommandHandler _handler;
        private readonly AlunoService _service;

        public AlunoCommandHandlerTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;
            var context = new AppDbContext(options);

            var repository = new AlunoRepository(context);

            _service = new AlunoService(repository);

            _handler = new AlunoCommandHandler(_service);
        }

        [Fact]
        public void Should_Handler_Insert_Aluno()
        {
            var command = new InsertAlunoCommand() { Nome = "Aluno", Email = "aluno@email.com.br", RA = "1234567750-ABC", CPF = "426.721.100-00" };

            var result = _handler.Handle(command, new CancellationToken()).Result;

            Assert.True(result);
        }


        [Fact]
        public void Should_Handler_Update_Aluno()
        {
            const string RA = "1234567751-ABC";

            InsertAluno(RA);

            var command = new UpdateAlunoCommand() { Nome = "Aluno2", Email = "aluno2@email.com.br", RA = RA };

            var result = _handler.Handle(command, new CancellationToken()).Result;

            Assert.True(result);
        }

        [Fact]
        public void Should_Handler_Delete_Aluno()
        {
            const string RA = "1234567752-ABC";

            InsertAluno(RA);

            var command = new DeleteAlunoCommand() { RA = RA };

            var result = _handler.Handle(command, new CancellationToken()).Result;

            Assert.True(result);
        }


        private void InsertAluno(string RA)
        {
            var command = new InsertAlunoCommand() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = "426.721.100-00" };
            _service.Insert(command).GetAwaiter();
        }
    }
}
