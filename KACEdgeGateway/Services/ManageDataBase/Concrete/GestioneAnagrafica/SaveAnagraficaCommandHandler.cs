using EdgeGateway.Services.ManageDataBase.Business;
using KVMWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;


namespace EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica
{
    public class SaveAnagraficaCommandHandler : ICommandHandler<SaveAnagraficaCommand, CommandResponse>
    {
        private readonly SaveAnagraficaCommand _command;
        private readonly AnagraficaBusiness _business;
        public SaveAnagraficaCommandHandler(SaveAnagraficaCommand command, AnagraficaBusiness business)
        {
            _command = command;
            _business = business;
        }
        public CommandResponse Execute()
        {
            return _business.Save(_command.Anagrafica);
        }
    }
}
