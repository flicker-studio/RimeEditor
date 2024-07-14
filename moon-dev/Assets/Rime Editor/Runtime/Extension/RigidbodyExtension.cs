using UnityEngine;

namespace LevelEditor.Extension
{
    /// <summary>
    ///     The axis direction of the Rigidbody that needs to be frozen.
    /// </summary>
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

    /// <summary>
    ///     A static extension method of Rigidbody.
    /// </summary>
    public static class RigidbodyExtension
    {
        /// <summary>
        ///     The method of freezing the Rigidbody2D axis.
        /// </summary>
        /// <param name="rigidbody2D"></param>
        /// <param name="freezeaxis"></param>
        public static void Freeze(this Rigidbody2D rigidbody2D, FREEZEAXIS freezeaxis)
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
}