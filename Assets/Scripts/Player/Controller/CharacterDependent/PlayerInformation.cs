using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : BaseInformation
{
    public MotionInputController MotionInputController;

    public ComponentController ComponentController;

    public CharacterProperty CharacterProperty;

    public PlayerColliding PlayerColliding;

    public PlayerRaycasting PlayerRaycasting;

    public PlayerInformation(Transform transform)
    {
        MotionInputController = new MotionInputController();
        ComponentController = new ComponentController(transform);
        CharacterProperty = Resources.Load<CharacterProperty>("GlobalSettings/CharacterProperty");
        PlayerColliding = new PlayerColliding(transform,CharacterProperty);
        PlayerRaycasting = new PlayerRaycasting(CharacterProperty,ComponentController);
    }
}
