using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnectivity : MonoBehaviour
{
    public Collider2D m_targetCollider;
    public List<Collider2D> m_connectCollider = new List<Collider2D>();

    private ContactFilter2D m_contactFilter2D = new ContactFilter2D();
    private HashSet<Collider2D> m_visited = new HashSet<Collider2D>();
    
    void FixedUpdate()
    {
        m_visited.Clear();
        m_connectCollider.Clear();
        CheckColliderConnectivity(m_targetCollider);
    }

    void CheckColliderConnectivity(Collider2D startCollider)
    {
        Stack<Collider2D> stack = new Stack<Collider2D>();
        stack.Push(startCollider);

        while (stack.Count > 0)
        {
            Collider2D current = stack.Pop();
            
            if (m_visited.Contains(current))
            {
                continue;
            }

            m_visited.Add(current);
            m_connectCollider.Add(current);

            List<Collider2D> collider2D = new List<Collider2D>();
            current.OverlapCollider(m_contactFilter2D, collider2D);

            foreach (Collider2D c in collider2D)
            {
                stack.Push(c);
            }
        }
    }
}
