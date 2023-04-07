using KACWebAPI.Domain.Command.Abstract;
using KVMWebAPI.Domain.Command.Commands;

namespace KACWebAPI.Domain.Command.Commands.Utente
{
    public class CreateTokenCommand : ICommand<CommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public CreateTokenCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}