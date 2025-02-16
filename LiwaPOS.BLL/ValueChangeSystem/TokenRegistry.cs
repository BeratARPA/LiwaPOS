using LiwaPOS.Shared.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LiwaPOS.BLL.ValueChangeSystem
{
    public class TokenRegistry
    {
        private readonly ConcurrentDictionary<string, ITokenHandler> _handlers;
        private readonly IServiceProvider _serviceProvider;

        public TokenRegistry(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _handlers = new ConcurrentDictionary<string, ITokenHandler>(
                StringComparer.OrdinalIgnoreCase);

            RegisterCoreHandlers();
        }

        private void RegisterCoreHandlers()
        {
            var handlers = _serviceProvider.GetServices<ITokenHandler>();
            foreach (var handler in handlers)
            {
                var attributes = handler.GetType()
                                        .GetCustomAttributes(typeof(TokenAttribute), true)
                                        .Cast<TokenAttribute>();

                foreach (var attr in attributes)
                {
                    foreach (var token in attr.Tokens)
                    {
                        _handlers.TryAdd(token, handler);
                    }
                }
            }
        }

        public string ResolveToken(string token, IReadOnlyList<string> args, ValueContext context)
        {
            if (_handlers.TryGetValue(token, out var handler))
            {
                return handler.Handle(token, args, context);
            }
            return null; // Token bulunamazsa null döner
        }
    }
}
