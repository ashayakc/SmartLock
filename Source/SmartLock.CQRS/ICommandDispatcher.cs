namespace SmartLock.CQRS
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TParameter>(TParameter command) where TParameter : ICommand;
    }
}
