using UnityEngine;

namespace Moon
{
    public class BoxCaster2D : MonoBehaviour
    {
        public Vector2 halfSize = Vector2.one * 0.5f;

        public Vector2 offset = Vector2.zero;

        public LayerMask targetLayer;

        public bool Cast()
        {
            var origin = transform.position;
            Vector2 direction = offset.normalized;
            var hit = Physics2D.BoxCast(origin, halfSize, 0, direction, offset.sqrMagnitude, targetLayer);
            return hit.collider != null;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (UnityEditor.Selection.activeObject != gameObject) return;

            var origin = transform.position;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(origin + (Vector3)offset, halfSize * 2);
        }
#endif
    }
}