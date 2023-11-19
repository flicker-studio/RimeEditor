using System.Collections.Generic;
using Frame.Static.Extensions;
using UnityEditor;
using UnityEngine;

namespace Frame.Tool.Pool
{
    //TODO: 考虑在未来使用UnityEngine.Pool代替
    public class ObjectPool : Singleton<ObjectPool>
    {
        private GameObject m_cachePanel;

        private Dictionary<string, GameObject> m_typeCachePanel = new Dictionary<string, GameObject>();

        private Dictionary<string, List<GameObject>> m_pool = new Dictionary<string, List<GameObject>>();

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
                m_objTag.Add(prefab, tag);
                m_pool[tag] = new List<GameObject>();
                m_outPool[tag] = new List<GameObject>();
            }

            GameObject obj;
            if (m_pool[tag].Count > 0)
            {
                obj = m_pool[tag][0];
                m_pool[tag].RemoveAt(0);
                obj.SetActive(true);
                obj.transform.SetParent(null);
            }
            else
            {
                obj = GameObject.Instantiate(prefab);
                obj.name = prefab.name + m_uniqueId++;
                GameObject.DontDestroyOnLoad(obj);
            }
            m_outPool[tag].Add(obj);
            ResetObject(obj, prefab);
            return obj;
        }

        /// <summary>
        /// 指定从对象池中拿取物体
        /// </summary>
        /// <param name="目标物体"></param>
        /// <param name="目标预制体"></param>
        public GameObject OnTake(GameObject targetObj, GameObject prefab)
        {
            string tag = prefab.name;
            if (!m_pool.ContainsKey(tag))
            {
                m_objTag.Add(prefab, tag);
                m_pool[tag] = new List<GameObject>();
                m_outPool[tag] = new List<GameObject>();
            }

            if (!m_pool[tag].Contains(targetObj))
            {
                return OnTake(prefab);
            }

            targetObj.SetActive(true);
            targetObj.transform.SetParent(null);
            m_pool[tag].Remove(targetObj);
            m_outPool[tag].Add(targetObj);
            ResetObject(targetObj, prefab);
            return targetObj;
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
                obj.transform.SetParent(m_typeCachePanel[tag].transform);
                obj.SetActive(false);
                m_pool[tag].Add(obj);
                m_outPool[tag].Remove(obj);
            }
        }

        /// <summary>
        /// 归还对象池对象并初始化对象池
        /// </summary>
        /// <param name="游戏对象"></param>
        /// <param name="预制体"></param>
        public void OnRelease(GameObject obj, GameObject prefab)
        {
            CheckCachePanel();

            if (obj == null)
            {
                return;
            }

            string tag = CheckTag(obj);
            if (!m_pool.ContainsKey(tag))
            {
                m_objTag.Add(prefab, tag);
                m_pool[tag] = new List<GameObject>();
                m_outPool[tag] = new List<GameObject>();
            }
            CheckTypeCachePanel(tag);
            obj.transform.parent = m_typeCachePanel[tag].transform;
            obj.SetActive(false);
            m_pool[tag].Add(obj);
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
                    if (tempObj.transform.childCount > 0) continue;
                    tempObj.transform.parent = m_typeCachePanel[tag].transform;
                    tempObj.SetActive(false);
                    m_pool[tag].Add(tempObj);
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
        public bool CompareObj(GameObject obj, GameObject prefab)
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

        private void ResetObject(GameObject obj,GameObject prefab)
        {
            foreach (var component in obj.GetComponents<Component>())
            {
                if (component is Collider2D collider)
                {
                    ResetColloder(collider,prefab.GetComponent<Collider2D>());
                }
            }
        }

        private void ResetColloder(Collider2D objColloder,Collider2D prefabCollider)
        {
            if(prefabCollider == null) return;
            objColloder.isTrigger = prefabCollider.isTrigger;
        }
    }

}