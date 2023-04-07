using EdgeGateway.Services.ManageDataBase.Business;
using KVMWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica
{
    public class AnagraficaCommandHandlerFactory
    {
        public static ICommandHandler<SaveAnagraficaCommand, CommandResponse> Build(SaveAnagraficaCommand command, AnagraficaBusiness business)
        {
            return new SaveAnagraficaCommandHandler(command, business);
        }

        public static ICommandHandler<DeleteAnagraficaCommand, CommandResponse> Build(DeleteAnagraficaCommand command, AnagraficaBusiness business)
        {
            return new DeleteAnagraficaCommandHandler(command, business);
        }
    }
}
