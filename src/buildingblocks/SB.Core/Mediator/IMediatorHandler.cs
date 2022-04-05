using FluentValidation.Results;
using SB.Core.Messages;
using System.Threading.Tasks;

namespace SB.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublisherEvent<T>(T eventMessage) where T : Event;

        Task<ValidationResult> PublisherCommand<T>(T command) where T : Command;
    }
}
