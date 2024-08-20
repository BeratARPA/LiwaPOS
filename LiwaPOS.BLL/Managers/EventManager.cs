namespace LiwaPOS.BLL.Managers
{
    public class EventManager
    {
        private readonly Dictionary<Type, List<Action<object>>> _eventHandlers = new Dictionary<Type, List<Action<object>>>();

        public void Subscribe<T>(Action<T> handler)
        {
            if (!_eventHandlers.ContainsKey(typeof(T)))
            {
                _eventHandlers[typeof(T)] = new List<Action<object>>();
            }
            _eventHandlers[typeof(T)].Add(e => handler((T)e));
        }

        public void Publish<T>(T eventArgs)
        {
            if (_eventHandlers.ContainsKey(typeof(T)))
            {
                foreach (var handler in _eventHandlers[typeof(T)])
                {
                    handler(eventArgs);
                }
            }
        }
    }
}
