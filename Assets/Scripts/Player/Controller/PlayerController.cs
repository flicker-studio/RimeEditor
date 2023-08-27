using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation
{
    public InputController InputController;

    public ComponentController ComponentController;

    public CharacterProperty CharacterProperty;

    public PlayerInformation(Transform transform)
    {
        InputController = new InputController();
        ComponentController = new ComponentController(transform);
        CharacterProperty = Resources.Load<CharacterProperty>("GlobalSettings/CharacterProperty");
    }
}

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

    private void Update()
    {
        m_motionController.Motion(m_playerInformation);
    }
}
