using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Credito.Application.Commands;
using SB.Credito.Domain.Repositories;
using SB.WebAPI.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace SB.Credito.API.Controllers
{
    [ApiController]
    [Route("api/creditos")]
    public class CreditoController : MainController
    {
        private readonly IMediator _mediator;
        private readonly ICreditoRepository _creditoRepository;

        public CreditoController(ICreditoRepository movimentationRepository, IMediator mediator)
        {
            _creditoRepository = movimentationRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var creditos = await _creditoRepository.GetCreditos();

            return Ok(creditos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCreditoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Errors.Count > 0)
                return CustomResponse(result);

            return CustomResponse(new CommandResponse("Crédito adicionando com sucesso"));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteCreditoCommand
            {
                Id = Guid.Parse(id)
            };

            var result = await _mediator.Send(command);

            if (result.Errors.Count > 0)
                return CustomResponse(result);

            return CustomResponse(new CommandResponse("Crédito deletado com sucesso"));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCreditoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Errors.Count > 0)
                return CustomResponse(result);

            return CustomResponse(new CommandResponse("Crédito atualizado com sucesso"));
        }
    }
}
