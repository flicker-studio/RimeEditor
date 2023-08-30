using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    private MotionController m_motionController;

    private PlayerInformation m_playerInformation;

    private void ControllerInit()
    {
        m_motionController = new MotionController();
        m_playerInformation = new PlayerInformation(transform);
    }
    
    private void Start()
    {
        ControllerInit();
        m_motionController.ChangeMotionState(new MainDefultState(m_playerInformation));
        m_motionController.ChangeMotionState(new AdditiveDefultState(m_playerInformation));
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
