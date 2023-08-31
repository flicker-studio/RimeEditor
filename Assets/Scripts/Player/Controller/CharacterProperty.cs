using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CharacterProperty",order = 1,fileName = "CharacterProperty")]
public class CharacterProperty : ScriptableObject
{
    [FormerlySerializedAs("m_playerMoveProperty")] public PlayerMoveProperty MoveProperty;
    
    [FormerlySerializedAs("m_PlayerJumpProperty")] public PlayerJumpProperty JumpProperty;
    
    [FormerlySerializedAs("m_GroundCheckParameter")] public PlayerGroundCheckParameter GroundCheckParameter;
    
    public PlayerCeilingCheckParameter CeilingCheckParameter;
    
    public PlayerOrthogonalOnGround OrthogonalOnGround;
    
    [Serializable]
    public struct PlayerMoveProperty
    {
        [CustomLabel("玩家最大水平移动速度"),Range(1,100)] 
        public float PLAYER_MOVE_SPEED;
        [CustomLabel("玩家最大水平疾跑速度"),Range(1,100)] 
        public float PLAYER_MOVE_RUN_SPEED;
        [CustomLabel("地面加速到最大水平速度时间"),Range(0,1)]
        public float GROUND_TIME_TO_MAXIMUN_SPEED;
        [CustomLabel("地面停止运动减速时间"),Range(0,1)]
        public float GROUND_TIME_TO_STOP;
        [CustomLabel("空中加速到最大水平速度时间"),Range(0,1)]
        public float AIR_TIME_TO_MAXIMUN_SPEED;
        [CustomLabel("空中停止运动减速时间"),Range(0,1)]
        public float AIR_TIME_TO_STOP;
        [CustomLabel("速度变化曲线")]
        public AnimationCurve ACCELERATION_CURVE;
    }
    
    [Serializable]
    public struct PlayerJumpProperty
    {
        [CustomLabel("玩家最大跳跃速度"),Range(1,100)]
        public float PLAYER_MAXIMAL_JUMP_SPEED;
        [FormerlySerializedAs("PLAYER_MAXIMAL_JUMP_TIMMER")] [CustomLabel("玩家最大跳跃时间"),Range(0,3)] 
        public float PLAYER_MAXIMAL_JUMP_TIME;
        [FormerlySerializedAs("PLAYER_SMALLEST_JUMP_TIMMER")] [CustomLabel("玩家最小跳跃时间"),Range(0,3)] 
        public float PLAYER_SMALLEST_JUMP_TIME;
        [CustomLabel("玩家跳跃结束速度补正"),Range(-100,0)]
        public float PLAYER_JUMP_FINISH_SPEED_COMPENSATION;
        [CustomLabel("跳跃缓冲时间"),Range(0,10)]
        public float JUMPING_BUFFER_TIME;
        [CustomLabel("土狼时间"),Range(0,1)]
        public float COYOTE_TIME;
        [CustomLabel("速度变化曲线")]
        public AnimationCurve ACCELERATION_CURVE;
    }
    
    [Serializable]
    public struct PlayerGroundCheckParameter
    {
        [CustomLabel("地面检测盒子大小")]
        public Vector3 CHECK_CAPSULE_SIZE;
        [FormerlySerializedAs("CHECK_CAPSULE_RELATIVE_POSITION")] [CustomLabel("地面检测盒相对位置")]
        public float CHECK_CAPSULE_RELATIVE_POSITION_Y;
        [CustomLabel("地面检测层级")]
        public LayerMask CHECK_LAYER;
    }
    
    [Serializable]
    public struct PlayerCeilingCheckParameter
    {
        [CustomLabel("顶头检测盒子大小")]
        public Vector3 CHECK_CAPSULE_SIZE;
        [FormerlySerializedAs("CHECK_CAPSULE_RELATIVE_POSITION")] [CustomLabel("顶头检测盒相对位置")]
        public float CHECK_CAPSULE_RELATIVE_POSITION_Y;
    }
    [Serializable]
    public struct PlayerOrthogonalOnGround
    {
        [CustomLabel("检测点数量"),Range(1,10)]
        public int CHECK_RAYCAST_POINTS;
        [CustomLabel("检测射线长度"),Range(0,10)]
        public float CHECK_RAYCAST_DISTANCE;
        [CustomLabel("发射线补偿坐标")]
        public Vector2 START_POINT_COMPENSATION;
    }
}
