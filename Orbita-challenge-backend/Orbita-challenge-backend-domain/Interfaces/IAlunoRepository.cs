using Orbita_challenge_backend_domain.Entities;

namespace Orbita_challenge_backend_domain.Interfaces
{
    public interface IAlunoRepository
    {
        void Delete(int id);
        void Insert(Aluno entity);
        void Update(Aluno entity);
        Task<Aluno> GetById(int id);
        Task<IEnumerable<Aluno>> GetAll();
    }
}
