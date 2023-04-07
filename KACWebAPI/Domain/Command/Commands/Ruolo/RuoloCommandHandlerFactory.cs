using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;


namespace KACWebAPI.Domain.Command.Commands.Ruolo
{
    public class RuoloCommandHandlerFactory
    {
        public static ICommandHandler<SaveRuoloCommand, CommandResponse> Build(SaveRuoloCommand command, RuoloBusiness business)
        {
            return new SaveRuoloCommandHandler(command, business);
        }

    }
}
