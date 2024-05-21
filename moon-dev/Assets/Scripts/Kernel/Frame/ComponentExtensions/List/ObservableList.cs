using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public sealed class ObservableList<T> : IEnumerable<T>
{
    [JsonProperty] private List<T>         list = new();
    public                 Action<T>       OnAdd;
    public                 Action<T>       OnRemove;
    public                 Action<List<T>> OnRemoveAll;
    public                 Action<List<T>> OnAddRange;

    public event Action OnClear;

    public int Count => list.Count;

    public T this[int index]
    {
        get => list[index];
        set => list[index] = value;
    }

    public void Add(T item)
    {
        list.Add(item);
        OnAdd?.Invoke(item);
    }

    public void AddRange(List<T> itemList)
    {
        list.AddRange(itemList);
        OnAddRange?.Invoke(itemList);
    }

    public bool Remove(T item)
    {
        var removed = list.Remove(item);
        if (removed) OnRemove?.Invoke(item);
        return removed;
    }

    public bool RemoveAll(List<T> itemList)
    {
        var removeCount = list.RemoveAll(item => itemList.Contains(item));
        if (removeCount > 0) OnRemoveAll?.Invoke(itemList);
        return false;
    }

    public void Clear()
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