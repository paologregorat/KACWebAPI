using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class SaveUtenteCommand : ICommand<CommandResponse>
    {
        public KACCloudContextLibrary.Domain.Entity.Utente Utente { get; private set; }
        public SaveUtenteCommand(KACCloudContextLibrary.Domain.Entity.Utente item)
        {
            Utente = item;
        }
    }
}