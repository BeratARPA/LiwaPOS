using System.Collections.Immutable;

namespace LiwaPOS.Shared.Models
{
    public readonly struct ValueContext
    {
        public readonly object SourceEntity; // Ticket, Order, Entity vb.
        public readonly ImmutableDictionary<string, object> Environment;
        public readonly DateTime Timestamp;

        public ValueContext(object source, ImmutableDictionary<string, object> env)
        {
            SourceEntity = source;
            Environment = env;
            Timestamp = DateTime.Now;
        }
    }
}
