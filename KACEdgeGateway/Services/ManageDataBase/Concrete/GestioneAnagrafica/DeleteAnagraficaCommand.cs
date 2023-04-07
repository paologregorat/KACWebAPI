using KVMWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica;

public class DeleteAnagraficaCommand : ICommand<CommandResponse>
{
    public Guid ID { get; private set; }
    public DeleteAnagraficaCommand(Guid id)
    {
        ID = id;
    }
}