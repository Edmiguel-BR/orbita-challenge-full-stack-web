using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_domain.Entities;
using Orbita_challenge_backend_domain.Interfaces;
using Orbita_challenge_backend_Infra.Context;

namespace Orbita_challenge_backend_Infra.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Insert(Aluno aluno)
        {
            _context.AddAsync(aluno);
            _context.SaveChanges();
        }

        public void Update(Aluno aluno)
        {
            _context.Update(aluno);
            _context.SaveChanges();
        }

        public void Delete(string ra)
        {
            var aluno = _context.Alunos.Find(ra);

            if (aluno != null)
            {
                _context.Remove(aluno);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Aluno>> GetAll()
        {
            return await _context.Alunos.ToListAsync();
        }

        public Task<Aluno> GetByRA(string ra)
        {
            return _context.Alunos.FirstOrDefaultAsync(a => a.RA == ra);
        }

        public Task<Aluno> GetByCPF(string cpf)
        {
            return _context.Alunos.FirstOrDefaultAsync(a => a.CPF == cpf);
        }
    }
}
