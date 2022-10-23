using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Handlers;
using Orbita_challenge_backend_Application.Queries;
using Orbita_challenge_backend_Application.Service;
using Orbita_challenge_backend_Infra.Context;
using Orbita_challenge_backend_Infra.Repositories;

namespace Orbita_challenge_backend_Tests.Application
{
    public class AlunoQueryHandlerTest
    {
        private const string CPF = "426.721.100-00";
        private readonly AlunoQueryHandler _handler;
        private readonly AlunoService _service;

        public AlunoQueryHandlerTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;
            var context = new AppDbContext(options);

            var repository = new AlunoRepository(context);

            _service = new AlunoService(repository);

            _handler = new AlunoQueryHandler(_service);
        }

        [Fact]
        public void Should_Handler_Get_All_Alunos()
        {
            const string RA = "1234567761-ABC";

            InsertAluno(RA);

            var query = new GetAllAlunosQuery();

            var result = _handler.Handle(query, new CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void Should_Handler_Get_By_CPF_Alunos()
        {
            const string RA = "1234567762-ABC";

            InsertAluno(RA);

            var query = new GetAlunoByCpfQuery() { CPF = CPF };

            var result = _handler.Handle(query, new CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.Equal(CPF, result.CPF);
        }

        [Fact]
        public void Should_Handler_Get_By_RA_Alunos()
        {
            const string RA = "1234567761-ABC";

            InsertAluno(RA);

            var query = new GetAlunoByRAQuery() { RA = RA };

            var result = _handler.Handle(query, new CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.Equal(RA, result.RA);
        }

        private void InsertAluno(string RA)
        {
            var command = new InsertAlunoCommand() { Nome = "Aluno", Email = "aluno@email.com.br", RA = RA, CPF = CPF };
            _service.Insert(command).GetAwaiter();
        }
    }
}
