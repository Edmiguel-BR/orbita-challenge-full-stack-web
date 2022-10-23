using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Queries.ViewModel;
using Orbita_challenge_backend_domain.Entities;

namespace Orbita_challenge_backend_Application.Mappings
{
    public static class AlunoMapper
    {
        public static Aluno ToEntity(InsertAlunoCommand command) => new() { Nome = command.Nome, Email = command.Email, CPF = command.CPF, RA = command.RA };

        public static AlunoViewModel ToResponse(Aluno entity) => entity != null ? new() { Nome = entity.Nome, CPF = entity.CPF, Email = entity.Email, RA = entity.RA } : null;

        public static IEnumerable<AlunoViewModel> ToResponse(IEnumerable<Aluno> alunos) => (from Aluno aluno in alunos
                                                                                            select ToResponse(aluno)).ToList();
    }
}
