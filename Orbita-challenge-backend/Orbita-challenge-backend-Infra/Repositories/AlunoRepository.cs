using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_domain.Entities;
using Orbita_challenge_backend_domain.Interfaces;
using Orbita_challenge_backend_Infra.Context;

namespace Orbita_challenge_backend_Infra.Repositories
{
    internal class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Insert(Aluno entity)
        {
            _context.AddAsync(entity);
        }

        public void Update(Aluno entity)
        {
            _context.Update(entity);
        }

        public void Delete(string ra)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.RA == ra);

            if (aluno != null)
            {
                _context.Remove(aluno);
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
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
