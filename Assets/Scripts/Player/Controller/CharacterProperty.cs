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
        [CustomLabel("玩家最大移动速度"),Range(1,10)] 
        public float PLAYER_MOVE_SPEED;
        [CustomLabel("地面加速曲线")]
        public AnimationCurve ACCELERATION_CURVE;
        [CustomLabel("地面减速曲线")] 
        public AnimationCurve RETARDATION_CURVE;
        [CustomLabel("空中加速曲线")]
        public AnimationCurve AIR_ACCELERATION_CURVE;
        [CustomLabel("空中减速曲线")] 
        public AnimationCurve AIR_RETARDATION_CURVE;
    }
    
    [Serializable]
    public struct PlayerJumpProperty
    {
        [CustomLabel("玩家最大跳跃时间"),Range(1,3)] 
        public float PLAYER_JUMP_TIMMER;
    }
}
