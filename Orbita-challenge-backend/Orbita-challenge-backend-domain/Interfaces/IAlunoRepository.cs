using Orbita_challenge_backend_domain.Entities;

namespace Orbita_challenge_backend_domain.Interfaces
{
    public interface IAlunoRepository
    {
        bool Delete(string ra);
        Task<bool> Insert(Aluno aluno);
        bool Update(Aluno aluno);
        Task<Aluno> GetByRA(string ra);
        Task<Aluno> GetByCPF(string cpf);
        Task<IEnumerable<Aluno>> GetAll();
    }
}
