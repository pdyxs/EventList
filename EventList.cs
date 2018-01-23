using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace System.Linq
{
    public static class EventListExtensions {
        public static void ForEach<T>(this EventList<T> list, Action<T> action) {
            foreach (T obj in list) {
                action.Invoke(obj);
            }
        }

        public static T Find<T>(this EventList<T> list, Predicate<T> match) {
            foreach (T o in list) {
                if (match.Invoke(o)) {
                    return o;
                }
            }
            return default(T);
        }
    }
}

public class EventList<T> : IList<T>, ICollection<T>, IEnumerable<T>
{
    private List<T> list = new List<T>();

    public UnityEvent OnCleared = new UnityEvent();

    public class TEvent : UnityEvent<T> { }
    public TEvent OnAdded = new TEvent();
    public TEvent OnRemoved = new TEvent();

    public class IndexTEvent : UnityEvent<int, T> { }
    public IndexTEvent OnInserted = new IndexTEvent();

    public class IndexEvent : UnityEvent<int> { }
    public IndexEvent OnRemovedAt = new IndexEvent();

    public T[] ToArray() {
        return list.ToArray();
    }

    public int Count
    {
        get
        {
            return ((IList<T>)list).Count;
        }
    }

    public bool IsReadOnly
    {
        get
        {
            return ((IList<T>)list).IsReadOnly;
        }
    }

    public bool IsFixedSize
    {
        get
        {
            return ((IList)list).IsFixedSize;
        }
    }

    public bool IsSynchronized
    {
        get
        {
            return ((IList)list).IsSynchronized;
        }
    }

    public object SyncRoot
    {
        get
        {
            return ((IList)list).SyncRoot;
        }
    }

    public T this[int index]
    {
        get
        {
            return ((IList<T>)list)[index];
        }
        set
        {
            ((IList<T>)list)[index] = value;
        }
    }

    public int IndexOf(T item)
    {
        return ((IList<T>)list).IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        ((IList<T>)list).Insert(index, item);
        OnInserted.Invoke(index, item);
    }

    public void RemoveAt(int index)
    {
        ((IList<T>)list).RemoveAt(index);
        OnRemovedAt.Invoke(index);
    }

    public void Add(T item)
    {
        ((IList<T>)list).Add(item);
        OnAdded.Invoke(item);
    }

    public void Clear()
    {
        ((IList<T>)list).Clear();
        OnCleared.Invoke();
    }

    public bool Contains(T item)
    {
        return ((IList<T>)list).Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        ((IList<T>)list).CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        if (((IList<T>)list).Remove(item))
        {
            OnRemoved.Invoke(item);
            return true;
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return ((IList<T>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IList<T>)list).GetEnumerator();
    }
}

