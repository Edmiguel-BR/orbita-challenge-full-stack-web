using Orbita_challenge_backend_domain.Entities;

namespace Orbita_challenge_backend_domain.Interfaces
{
    public interface IAlunoRepository
    {
        void Delete(string ra);
        void Insert(Aluno aluno);
        void Update(Aluno aluno);
        Task<Aluno> GetByRA(string ra);
        Task<Aluno> GetByCPF(string cpf);
        Task<IEnumerable<Aluno>> GetAll();
    }
}
