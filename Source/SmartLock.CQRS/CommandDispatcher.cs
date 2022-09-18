using Microsoft.Extensions.DependencyInjection;

namespace SmartLock.CQRS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider myServiceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            myServiceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = GetCommandHandler<TParameter>();
            await ExecuteAsync(handler, command);
        }

        private ICommandHandler<TParameter> GetCommandHandler<TParameter>() where TParameter : ICommand
        {
            return myServiceProvider.GetRequiredService<ICommandHandler<TParameter>>();
        }

        private async Task ExecuteAsync<TParameter>(ICommandHandler<TParameter> handler, TParameter command) where TParameter : ICommand
        {
            await handler.ExecuteAsync(command);
        }
    }
}
