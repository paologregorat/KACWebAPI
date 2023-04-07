using KACGatewayContextLibrary.Domain.Entity;
using KVMWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica;

public class SaveAnagraficaCommand : ICommand<CommandResponse>
{
    public Anagrafica Anagrafica { get; private set; }
    public SaveAnagraficaCommand(Anagrafica item)
    {
        Anagrafica = item;
    }
}