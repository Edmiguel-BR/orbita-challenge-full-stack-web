using MediatR;
using Orbita_challenge_backend_Application.Queries;
using Orbita_challenge_backend_Application.Queries.ViewModel;
using Orbita_challenge_backend_Application.Service;

namespace Orbita_challenge_backend_Application.Handlers
{
    public class AlunoQueryHandler :
        IRequestHandler<GetAllAlunosQuery, IEnumerable<AlunoViewModel>>,
        IRequestHandler<GetAlunoByCpfQuery, AlunoViewModel>,
        IRequestHandler<GetAlunoByRAQuery, AlunoViewModel>

    {
        private readonly AlunoService _service;

        public AlunoQueryHandler(AlunoService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<AlunoViewModel>> Handle(GetAllAlunosQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAll();
        }

        public async Task<AlunoViewModel> Handle(GetAlunoByCpfQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByCpf(request);
        }

        public async Task<AlunoViewModel> Handle(GetAlunoByRAQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByRA(request);
        }
    }
}
