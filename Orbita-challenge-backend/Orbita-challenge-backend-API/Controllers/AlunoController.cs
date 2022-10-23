using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orbita_challenge_backend_Application.Commands;
using Orbita_challenge_backend_Application.Queries;
using Orbita_challenge_backend_Infra.Exceptions;
using System.Threading.Tasks;

namespace Orbita_challenge_backend_API.Controllers
{
    [Route("aluno")]
    public class AlunoController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AlunoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Commands
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertAlunoCommand command)
        {
            return await Result(command);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAlunoCommand command)
        {
            return await Result(command);
        }

        [HttpDelete("{ra}")]
        public async Task<IActionResult> Delete(string ra)
        {
            var command = new DeleteAlunoCommand() { RA = ra };

            return await Result(command);
        }

        // Queries
        [HttpGet]
        [Route("ra")]
        public async Task<IActionResult> Get([FromQuery] GetAlunoByRAQuery query)
        {
            return await Result(query);
        }

        [HttpGet]
        [Route("cpf")]
        public async Task<IActionResult> Get([FromQuery] GetAlunoByCpfQuery query)
        {
            return await Result(query);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAlunosQuery();

            return await Result(query);
        }

        private async Task<IActionResult> Result(IBaseRequest request)
        {
            try
            {
                var retorno = await _mediator.Send(request);

                return retorno == null ? NotFound("Registro não encontrado") : Ok(retorno);
            }
            catch (ValidationDataException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
