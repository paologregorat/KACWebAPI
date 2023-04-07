using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class AdministratorCambiaPasswordCommandHandler : ICommandHandler<AdministratorCambiaPasswordCommand, CommandResponse>
    {
        private readonly AdministratorCambiaPasswordCommand _command;
        private readonly UtenteBusiness _business;
        public AdministratorCambiaPasswordCommandHandler(AdministratorCambiaPasswordCommand command, UtenteBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.AdministratorCambiaPassword(_command);
        }
    }
}
