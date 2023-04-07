using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class CreateTokenCommandHandler : ICommandHandler<CreateTokenCommand, CommandResponse>
    {
        private readonly CreateTokenCommand _command;
        private readonly UtenteBusiness _business;
        public CreateTokenCommandHandler(CreateTokenCommand command, UtenteBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.CreteToken(_command);
        }
    }
}
