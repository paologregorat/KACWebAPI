using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class SaveUtenteCommandHandler : ICommandHandler<SaveUtenteCommand, CommandResponse>
    {
        private readonly SaveUtenteCommand _command;
        private readonly UtenteBusiness _business;
        public SaveUtenteCommandHandler(SaveUtenteCommand command, UtenteBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.Save(_command);
        }
    }
}
