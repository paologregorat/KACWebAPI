using KACCloudContextLibrary.Domain.EntityNoDB;
using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class AdministratorCambiaPasswordCommand : ICommand<CommandResponse>
    {
        public AdministratorCambioPassword CambioPassword { get; private set; }
        public AdministratorCambiaPasswordCommand(AdministratorCambioPassword cambioPassword)
        {
            CambioPassword = cambioPassword;
        }
    }
}