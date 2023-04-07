namespace KVMWebAPI.Domain.Command.Abstract
{
    public interface ICommandHandler<in TCommand, out TResult> where TCommand : ICommand<TResult>
    {
        TResult Execute();
    }
}
