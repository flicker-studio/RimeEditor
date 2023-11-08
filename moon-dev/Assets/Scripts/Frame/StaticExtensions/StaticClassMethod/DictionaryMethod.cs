using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryMethod
{
    public static void AddRange<TKey,TValue>(this Dictionary<TKey,TValue> targetDic,Dictionary<TKey,TValue> addDic) 
        where TKey : class where TValue : class
    {
        foreach (var keyValuePair in addDic)
        {
            targetDic.Add(keyValuePair.Key,keyValuePair.Value);
        }
    }

    public static List<TKey> GetKeys<TKey,TValue>(this Dictionary<TKey,TValue> targetDic)
    {
        List<TKey> tempList = new List<TKey>();
        foreach (var keyValuePair in targetDic)
        {
            tempList.Add(keyValuePair.Key);
        }
        return tempList;
    }
    
    public static List<TValue> GetValues<TKey,TValue>(this Dictionary<TKey,TValue> targetDic)
    {
        List<TValue> tempList = new List<TValue>();
        foreach (var keyValuePair in targetDic)
        {
            tempList.Add(keyValuePair.Value);
        }
        return tempList;
    }
}
