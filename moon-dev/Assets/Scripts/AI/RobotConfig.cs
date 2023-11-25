using UnityEngine;
using UnityEngine.Serialization;

namespace Moon
{
    [CreateAssetMenu(fileName = "RobotConfig", menuName = "Moon/RobotConfig")]
    public sealed class RobotConfig : ScriptableObject
    {
        public LayerMask wallLayer; // 墙的层级
        public LayerMask blockLayer; // 障碍物的层级
        public float facingCheckDistance = 0.6f; // 检测墙壁的距离 从中心进行检测
        public float patrolSpeed = 1.0f; // 巡逻速度
        public string playerTag = "Player"; // 玩家的标签
    }
}