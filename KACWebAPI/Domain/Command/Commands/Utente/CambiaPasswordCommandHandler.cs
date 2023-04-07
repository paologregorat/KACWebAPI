using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class CambiaPasswordCommandHandler : ICommandHandler<CambiaPasswordCommand, CommandResponse>
    {
        private readonly CambiaPasswordCommand _command;
        private readonly UtenteBusiness _business;
        public CambiaPasswordCommandHandler(CambiaPasswordCommand command, UtenteBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.CambiaPassword(_command);
        }
    }
}
