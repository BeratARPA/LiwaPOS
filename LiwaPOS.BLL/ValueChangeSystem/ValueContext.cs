using Jint;
using System.Collections.Immutable;

namespace LiwaPOS.BLL.ValueChangeSystem
{
    public struct ValueContext
    {
        public object SourceEntity; // Ticket, Order, Entity vb.
        public ImmutableDictionary<string, object> Environment;
        public DateTime Timestamp;
        public Engine JavaScriptEngine;

        public ValueContext(object source, ImmutableDictionary<string, object> env)
        {
            SourceEntity = source;
            Environment = env;
            Timestamp = DateTime.Now;
            JavaScriptEngine = null!;
        }
    }
}
