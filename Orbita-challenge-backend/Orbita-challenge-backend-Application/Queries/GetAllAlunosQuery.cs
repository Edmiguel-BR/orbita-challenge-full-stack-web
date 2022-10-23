using MediatR;
using Orbita_challenge_backend_Application.Queries.ViewModel;

namespace Orbita_challenge_backend_Application.Queries
{
    public class GetAllAlunosQuery : IRequest<IEnumerable<AlunoViewModel>>
    {
    }
}
