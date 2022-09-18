namespace SmartLock.CQRS
{
    public interface ICommandHandler<TParameter> where TParameter : ICommand
    {
        Task ExecuteAsync(TParameter command);
    }
}
