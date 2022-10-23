using MediatR;
using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Service;

namespace Orbita_challenge_backend_Application.Handlers
{
    public class AlunoCommandHandler :
        IRequestHandler<InsertAlunoCommand, bool>,
        IRequestHandler<UpdateAlunoCommand, bool>,
        IRequestHandler<DeleteAlunoCommand, bool>
    {

        private readonly AlunoService _service;

        public AlunoCommandHandler(AlunoService service)
        {
            _service = service;
        }

        public Task<bool> Handle(InsertAlunoCommand command, CancellationToken cancellationToken)
        {
            return _service.Insert(command);
        }

        public Task<bool> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _service.Update(request));
        }

        public Task<bool> Handle(DeleteAlunoCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _service.Delete(request));
        }
    }
}
