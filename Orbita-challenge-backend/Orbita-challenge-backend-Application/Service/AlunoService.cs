using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Mappings;
using Orbita_challenge_backend_Application.Queries;
using Orbita_challenge_backend_Application.Queries.ViewModel;
using Orbita_challenge_backend_domain.Interfaces;
using Orbita_challenge_backend_Infra.Exceptions;

namespace Orbita_challenge_backend_Application.Service
{
    public class AlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Insert(InsertAlunoCommand command)
        {
            command.Validate(out var validationResult);

            if (!validationResult.IsValid)
            {
                throw new ValidationDataException(validationResult.Errors);
            }

            var entity = AlunoMapper.ToEntity(command);

            return await _repository.Insert(entity);
        }

        public bool Update(UpdateAlunoCommand command)
        {
            command.Validate(out var validationResult);

            if (!validationResult.IsValid)
            {
                throw new ValidationDataException(validationResult.Errors);
            }

            var entity = _repository.GetByRA(command.RA).Result;

            if (entity == null)
            {
                throw new ValidationDataException($"Aluno não encontrado: {command.RA}");
            }

            entity.Nome = command.Nome;
            entity.Email = command.Email;

            return _repository.Update(entity);
        }

        public bool Delete(DeleteAlunoCommand command)
        {
            command.Validate(out var validationResult);

            if (!validationResult.IsValid)
            {
                throw new ValidationDataException(validationResult.Errors);
            }

            return _repository.Delete(command.RA);
        }

        public async Task<IEnumerable<AlunoViewModel>> GetAll()
        {
            var alunos = await _repository.GetAll();

            return AlunoMapper.ToResponse(alunos);
        }

        public async Task<AlunoViewModel> GetByCpf(GetAlunoByCpfQuery query)
        {
            query.Validate(out var validationResult);

            if (!validationResult.IsValid)
            {
                throw new ValidationDataException(validationResult.Errors);
            }

            var aluno = await _repository.GetByCPF(query.CPF);

            return AlunoMapper.ToResponse(aluno);
        }

        public async Task<AlunoViewModel> GetByRA(GetAlunoByRAQuery query)
        {
            query.Validate(out var validationResult);

            if (!validationResult.IsValid)
            {
                throw new ValidationDataException(validationResult.Errors);
            }

            var aluno = await _repository.GetByRA(query.RA);

            return AlunoMapper.ToResponse(aluno);
        }

    }
}
