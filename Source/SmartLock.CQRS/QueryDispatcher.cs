using Microsoft.Extensions.DependencyInjection;

namespace SmartLock.CQRS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider myServiceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            myServiceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = GetQueryHandler<TParameter, TResult>();
            return await RetrieveAsync(handler, query);
        }

        private IQueryHandler<TParameter, TResult> GetQueryHandler<TParameter, TResult>()
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            return myServiceProvider.GetRequiredService<IQueryHandler<TParameter, TResult>>();
        }

        private async Task<TResult> RetrieveAsync<TParameter, TResult>(IQueryHandler<TParameter, TResult> handler, TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            return await handler.RetrieveAsync(query);
        }

    }
}
