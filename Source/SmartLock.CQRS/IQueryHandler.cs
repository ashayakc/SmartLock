namespace SmartLock.CQRS
{
    public interface IQueryHandler<TParameter, TResult> where TParameter : IQuery where TResult : IQueryResult
    {
        Task<TResult> RetrieveAsync(TParameter query);
    }
}
