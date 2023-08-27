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
        [CustomLabel("玩家移动速度"),Range(1,10)] 
        public float PLAYER_MOVE_SPEED;
    }
    
    [Serializable]
    public struct PlayerJumpProperty
    {
        [CustomLabel("玩家最大跳跃时间"),Range(1,3)] 
        public float PLAYER_JUMP_TIMMER;
    }
}
