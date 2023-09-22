using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    private MotionController m_motionController;

    private PlayerInformation m_playerInformation;

    private void ControllerInit()
    {
        m_playerInformation = new PlayerInformation(transform);
        m_motionController = new MotionController(m_playerInformation);
    }

    private void MotionInit()
    {
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.PlyerMainDefultState);
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.PlayerAdditiveDefultState);
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.PlayerPerpendicularGroundState);
    }
    private void Start()
    {
        ControllerInit();
        MotionInit();
    }

    private void FixedUpdate()
    {
        m_motionController.Motion(m_playerInformation);
    }

    private void OnDrawGizmos()
    {
        CharacterProperty temp = Resources.Load<CharacterProperty>("GlobalSettings/CharacterProperty");
        
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position+transform.up * temp.GroundCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y,
            temp.GroundCheckParameter.CHECK_CAPSULE_SIZE);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position+transform.up * temp.CeilingCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y,
            temp.CeilingCheckParameter.CHECK_CAPSULE_SIZE);
    }
}
