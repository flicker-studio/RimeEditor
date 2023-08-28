using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CharacterProperty",order = 1,fileName = "CharacterProperty")]
public class CharacterProperty : ScriptableObject
{
    public PlayerMoveProperty m_playerMoveProperty;

    public PlayerJumpProperty m_PlayerJumpProperty;
    
    [Serializable]
    public struct PlayerMoveProperty
    {
        [CustomLabel("玩家最大水平移动速度"),Range(1,10)] 
        public float PLAYER_MOVE_SPEED;
        [CustomLabel("玩家最大水平疾跑速度"),Range(1,10)] 
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
        [CustomLabel("玩家最大跳跃时间"),Range(1,3)] 
        public float PLAYER_JUMP_TIMMER;
    }
}
