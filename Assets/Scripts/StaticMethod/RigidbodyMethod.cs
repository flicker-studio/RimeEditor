using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FREEZEAXIS
{
    None,
    PosX,
    PosY,
    RotZ,
    PosXAndRotZ,
    PosYAndRotZ,
    PosXAndPosY,
    All
}

public static class RigidbodyMethod
{
    public static void Freeze(this Rigidbody2D rigidbody2D,FREEZEAXIS freezeaxis)
    {
        switch (freezeaxis)
        {
            case FREEZEAXIS.None:
                rigidbody2D.constraints = RigidbodyConstraints2D.None;
                break;
            case FREEZEAXIS.PosX:
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case FREEZEAXIS.PosY:
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                break;
            case FREEZEAXIS.RotZ:
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
            case FREEZEAXIS.PosXAndPosY:
                rigidbody2D.constraints =
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                break;
            case FREEZEAXIS.PosXAndRotZ:
                rigidbody2D.constraints =
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                break;
            case FREEZEAXIS.PosYAndRotZ:
                rigidbody2D.constraints =
                    RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                break;
            case FREEZEAXIS.All:
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
        }
    }
}
