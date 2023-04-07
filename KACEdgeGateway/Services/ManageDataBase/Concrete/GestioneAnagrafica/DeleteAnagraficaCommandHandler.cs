using EdgeGateway.Services.ManageDataBase.Business;
using KVMWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;


namespace EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica
{
    public class DeleteAnagraficaCommandHandler : ICommandHandler<DeleteAnagraficaCommand, CommandResponse>
    {
        private readonly DeleteAnagraficaCommand _command;
        private readonly AnagraficaBusiness _business;
        public DeleteAnagraficaCommandHandler(DeleteAnagraficaCommand command, AnagraficaBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.Delete(_command.ID);
        }
    }
}
