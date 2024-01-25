using UnityEngine;

namespace Moon
{
    [CreateAssetMenu(fileName = "RobotConfig", menuName = "Moon/RobotConfig")]
    public sealed class RobotConfig : ScriptableObject
    {
        public LayerMask wallLayer; // 墙的层级

        public LayerMask playerLayer; // 玩家的层级

        public LayerMask groundLayer; // 地面的层级

        public float facingCheckDistance = 0.6f; // 检测墙壁的距离 从中心进行检测

        public float patrolSpeed = 1.0f; // 巡逻速度

        public string playerTag = "Player"; // 玩家的标签

        public float topOffsetY = 0.5f; // 头部的偏移量

        public float groundCheckDistance = 0.6f; // 检测地面的距离 从中心进行检测

        public Vector2 followDistance = new Vector2(1.0f, 0.5f); // 当超过这个距离时就会跟随玩家

        public float followSpeed = 5f;
#if UNITY_EDITOR
        private void OnValidate()
        {
            followDistance.x = Mathf.Abs(followDistance.x);
            followDistance.y = Mathf.Abs(followDistance.y);
        }
#endif
    }
}