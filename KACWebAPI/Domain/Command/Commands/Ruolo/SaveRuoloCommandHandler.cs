using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Ruolo
{
    public class SaveRuoloCommandHandler : ICommandHandler<SaveRuoloCommand, CommandResponse>
    {
        private readonly SaveRuoloCommand _command;
        private readonly RuoloBusiness _business;
        public SaveRuoloCommandHandler(SaveRuoloCommand command, RuoloBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.Save(_command.Ruolo);
        }
    }
}
