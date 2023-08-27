using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CharacterProperty",order = 1,fileName = "CharacterProperty")]
public class CharacterProperty : ScriptableObject
{
    public PlayerMoveProperty m_playerMoveProperty;
    
    [Serializable]
    public struct PlayerMoveProperty
    {
        [CustomLabel("玩家移动速度"),Range(1,10)] 
        public float PLAYER_MOVE_SPEED;
    }
}
