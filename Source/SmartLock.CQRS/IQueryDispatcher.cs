namespace SmartLock.CQRS
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query) where TParameter : IQuery where TResult : IQueryResult;
    }
}
