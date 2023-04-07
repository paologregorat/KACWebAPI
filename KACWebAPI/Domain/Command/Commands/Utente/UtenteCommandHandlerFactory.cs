using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;


namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class UtenteCommandHandlerFactory
    {
        public static ICommandHandler<CreateTokenCommand, CommandResponse> Build(CreateTokenCommand command, UtenteBusiness business)
        {
            return new CreateTokenCommandHandler(command, business);
        }

        public static ICommandHandler<SaveUtenteCommand, CommandResponse> Build(SaveUtenteCommand command, UtenteBusiness business)
        {
            return new SaveUtenteCommandHandler(command, business);
        }

        public static ICommandHandler<CambiaPasswordCommand, CommandResponse> Build(CambiaPasswordCommand command, UtenteBusiness business)
        {
            return new CambiaPasswordCommandHandler(command, business);
        }

        public static ICommandHandler<AdministratorCambiaPasswordCommand, CommandResponse> Build(AdministratorCambiaPasswordCommand command, UtenteBusiness business)
        {
            return new AdministratorCambiaPasswordCommandHandler(command, business);
        }

    }
}
