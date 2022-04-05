using FluentValidation.Results;
using MediatR;
using SB.Core.Messages;
using System.Threading.Tasks;

namespace SB.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> PublisherCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task PublisherEvent<T>(T eventMessage) where T : Event
        {
            await _mediator.Publish(eventMessage);
        }
    }
}
