using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventObject<T>
    where T : class
{

    public class TEvent : UnityEvent<T> {}

    public TEvent OnChanged = new TEvent();

    private T obj;

    public T val {
        get {
            return obj;
        }
        set {
            if (!EqualityComparer<T>.Default.Equals(obj, value)) {
                obj = value;
                OnChanged.Invoke(obj);
            }
        }
    }

    public bool Exists => obj != null;
}
