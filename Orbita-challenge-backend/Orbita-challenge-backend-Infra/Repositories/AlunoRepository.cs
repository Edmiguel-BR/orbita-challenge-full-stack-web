using Microsoft.EntityFrameworkCore;
using Orbita_challenge_backend_domain.Entities;
using Orbita_challenge_backend_domain.Interfaces;
using Orbita_challenge_backend_Infra.Context;
using Orbita_challenge_backend_Infra.Exceptions;

namespace Orbita_challenge_backend_Infra.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Insert(Aluno aluno)
        {
            if (Existis(aluno))
            {
                throw new ValidationDataException($"Aluno já cadastrado: {aluno.RA}");
            }

            await _context.AddAsync(aluno);
            return Commit();
        }

        public bool Update(Aluno aluno)
        {

            if (!Existis(aluno))
            {
                throw new ValidationDataException($"Aluno não encontrado: {aluno.RA}");
            }

            _context.Update(aluno);
            return Commit();
        }

        public bool Delete(string ra)
        {
            var aluno = _context.Alunos.Find(ra);

            if (aluno == null)
            {
                throw new ValidationDataException($"Aluno não encontrado: {ra}");
            }

            _context.Alunos.Remove(aluno);
            return Commit();
        }

        public async Task<IEnumerable<Aluno>> GetAll()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<Aluno> GetByRA(string ra)
        {
            return await _context.Alunos.FirstOrDefaultAsync(a => a.RA == ra);
        }

        public async Task<Aluno> GetByCPF(string cpf)
        {
            return await _context.Alunos.FirstOrDefaultAsync(a => a.CPF == cpf);
        }
        private bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
        private bool Existis(Aluno aluno)
        {
            return _context.Alunos.Find(aluno.RA) != null;
        }

    }
}
