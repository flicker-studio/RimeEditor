using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation
{
    public InputController InputController;

    public ComponentController ComponentController;

    public CharacterProperty CharacterProperty;

    public PlayerColliding PlayerColliding;

    public PlayerRaycasting PlayerRaycasting;

    public PlayerInformation(Transform transform)
    {
        InputController = new InputController();
        ComponentController = new ComponentController(transform);
        CharacterProperty = Resources.Load<CharacterProperty>("GlobalSettings/CharacterProperty");
        PlayerColliding = new PlayerColliding(transform,CharacterProperty);
        PlayerRaycasting = new PlayerRaycasting(CharacterProperty,ComponentController);
    }
}
