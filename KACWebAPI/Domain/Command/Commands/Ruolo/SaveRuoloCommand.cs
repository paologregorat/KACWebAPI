using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Ruolo
{
    public class SaveRuoloCommand : ICommand<CommandResponse>
    {
        public KACCloudContextLibrary.Domain.Entity.Ruolo Ruolo { get; private set; }
        public SaveRuoloCommand(KACCloudContextLibrary.Domain.Entity.Ruolo item)
        {
            Ruolo = item;
        }
    }
}