using KACCloudContextLibrary.Domain.EntityNoDB;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class CambiaPasswordCommand : ICommand<CommandResponse>
    {
        public CambioPassword CambioPassword { get; private set; }
        public CambiaPasswordCommand(CambioPassword cambioPassword)
        {
            CambioPassword = cambioPassword;
        }
    }
}