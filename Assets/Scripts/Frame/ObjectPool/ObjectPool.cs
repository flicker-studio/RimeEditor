using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    private GameObject m_cachePanel;

    private Dictionary<string, GameObject> m_typeCachePanel = new Dictionary<string, GameObject>();

    private Dictionary<string, Queue<GameObject>> m_pool = new Dictionary<string, Queue<GameObject>>();
    
    private Dictionary<string, List<GameObject>> m_outPool = new Dictionary<string, List<GameObject>>();

    private Dictionary<GameObject, string> m_objTag = new Dictionary<GameObject, string>();

    private int m_uniqueId = 0;
    
    /// <summary>
    /// 清空对象池
    /// </summary>
    public void ClearPool()
    {
        m_uniqueId = 0;
        m_pool.Clear();
        m_objTag.Clear();
        m_outPool.Clear();
    }
    /// <summary>
    /// 向对象池请求对象
    /// </summary>
    /// <param name="预制体对象"></param>
    /// <returns>游戏对象</returns>
    public GameObject OnTake(GameObject prefab)
    {
        string tag = prefab.name;
        if (!m_pool.ContainsKey(tag))
        {
            m_objTag.Add(prefab,tag);
            m_pool[tag] = new Queue<GameObject>();
            m_outPool[tag] = new List<GameObject>();
        }

        GameObject obj;
        if (m_pool[tag].Count > 0)
        {
            obj = m_pool[tag].Dequeue();
            obj.SetActive(true);
            obj.transform.parent = null;
        }
        else
        {
            obj = GameObject.Instantiate(prefab);
            obj.name = prefab.name + m_uniqueId++;
            GameObject.DontDestroyOnLoad(obj);
        }
        m_outPool[tag].Add(obj);
        return obj;
    }
    /// <summary>
    /// 归还对象池对象
    /// </summary>
    /// <param name="游戏对象"></param>
    public void OnRelease(GameObject obj)
    {
        CheckCachePanel();
        
        if (obj == null)
        {
            return;
        }

        string tag = CheckTag(obj);
        if (m_pool.ContainsKey(tag))
        {
            CheckTypeCachePanel(tag);
            obj.transform.parent = m_typeCachePanel[tag].transform;
            obj.SetActive(false);
            m_pool[tag].Enqueue(obj);
            m_outPool[tag].Remove(obj);
        }
    }
    
    /// <summary>
    /// 归还对象池对象并初始化对象池
    /// </summary>
    /// <param name="游戏对象"></param>
    /// <param name="预制体"></param>
    public void OnRelease(GameObject obj,GameObject prefab)
    {
        CheckCachePanel();
        
        if (obj == null)
        {
            return;
        }

        string tag = CheckTag(obj);
        if (!m_pool.ContainsKey(tag))
        {
            m_objTag.Add(prefab,tag);
            m_pool[tag] = new Queue<GameObject>();
            m_outPool[tag] = new List<GameObject>();
        }
        CheckTypeCachePanel(tag);
        obj.transform.parent = m_typeCachePanel[tag].transform;
        obj.SetActive(false);
        m_pool[tag].Enqueue(obj);
        m_outPool[tag].Remove(obj);
    }
    
    /// <summary>
    /// 归还对象池同一类的所有对象，不归还拥有子物体的对象
    /// </summary>
    /// <param name="游戏对象"></param>
    public void OnReleaseAll(GameObject obj)
    {
        CheckCachePanel();
        
        if (obj == null)
        {
            return;
        }

        string tag = CheckTag(obj);
        if (m_pool.ContainsKey(tag))
        {
            CheckTypeCachePanel(tag);
            List<GameObject> tempList = new List<GameObject>();
            tempList.AddRange(m_outPool[tag]);
            foreach (var tempObj in tempList)
            {
                if(tempObj.transform.childCount > 0) continue;
                tempObj.transform.parent = m_typeCachePanel[tag].transform;
                tempObj.SetActive(false);
                m_pool[tag].Enqueue(tempObj);
                m_outPool[tag].Remove(tempObj);
            }
        }
    }
    /// <summary>
    /// 比较物体和预制体是否同一类型
    /// </summary>
    /// <param name="物体"></param>
    /// <param name="预制体"></param>
    /// <returns></returns>
    public bool CompareObj(GameObject obj,GameObject prefab)
    {
        return CheckTag(obj).Equals(prefab.name);
    }
    /// <summary>
    /// 检查实例物体标签
    /// </summary>
    /// <param name="实例物体"></param>
    /// <returns></returns>
    private string CheckTag(GameObject obj)
    {
        return obj.name.RemoveTrailingNumbers();
    }

    private void CheckTypeCachePanel(string tag)
    {
        if (!m_typeCachePanel.ContainsKey(tag))
        {
            m_typeCachePanel[tag] = new GameObject();
            m_typeCachePanel[tag].name = tag + "CachePanel";
            m_typeCachePanel[tag].transform.parent = m_cachePanel.transform;
        }
    }

    private void CheckCachePanel()
    {
        if (m_cachePanel == null)
        {
            m_cachePanel = new GameObject();
            m_cachePanel.name = "CachePanel";
            GameObject.DontDestroyOnLoad(m_cachePanel);
        }
    }
}
