using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
[JsonObject(MemberSerialization.OptIn)]
public class ObservableList<T> : IEnumerable<T>
{
    [JsonProperty]
    protected List<T> list = new List<T>();
    public event Action<T> OnAdd;
    public event Action<T> OnRemove;
    public event Action<List<T>> OnRemoveAll;
    public event Action<List<T>> OnAddRange;

    public event Action OnClear;
    
    public int Count
    {
        get
        {
            return list.Count;
        }
    }

    public T this[int index]
    {
        get
        {
            return list[index];
        }
        set
        {
            list[index] = value;
        }
    }
    public virtual void Add(T item)
    {
        list.Add(item);
        OnAdd?.Invoke(item);
    }

    public virtual void AddRange(List<T> itemList)
    {
        list.AddRange(itemList);
        OnAddRange?.Invoke(itemList);
    }

    public virtual bool Remove(T item)
    {
        bool removed = list.Remove(item);
        if (removed)
        {
            OnRemove?.Invoke(item);
        }
        return removed;
    }
    
    public virtual bool RemoveAll(List<T> itemList)
    {
        int removeCount = list.RemoveAll(item => itemList.Contains(item));
        if (removeCount > 0)
        {
            OnRemoveAll?.Invoke(itemList);
        }
        return false;
    }

    public virtual void Clear()
    {
        list.Clear();
        OnClear?.Invoke();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}