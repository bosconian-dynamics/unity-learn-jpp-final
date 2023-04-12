using System;
using System.Collections.Generic;

public class EventManager
{
    private Dictionary<Type, Delegate> eventTable = new Dictionary<Type, Delegate>();

    public void On<T>(Action<T> handler) where T : class
    {
        if (!eventTable.ContainsKey(typeof(T)))
            eventTable[typeof(T)] = null;

        eventTable[typeof(T)] = (Action<T>)eventTable[typeof(T)] + handler;
    }

    public void Off<T>(Action<T> handler) where T : class
    {
        if (eventTable.ContainsKey(typeof(T)))
            eventTable[typeof(T)] = (Action<T>)eventTable[typeof(T)] - handler;
    }

    public void Trigger<T>(T args) where T : class
    {
        Delegate handler;
        if (eventTable.TryGetValue(typeof(T), out handler))
            ((Action<T>)handler)?.Invoke(args);
    }
}
