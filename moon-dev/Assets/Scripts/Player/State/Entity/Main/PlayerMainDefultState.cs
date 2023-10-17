using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;

namespace Character
{
    public class PlayerMainDefultState : PlayerMainMotionState
{
    private float timmer = 0;

    private float m_oriSpeed;

    #region GetProperty

    private bool CheckSuitableSlope => m_playerInformation.CheckSuitableSlope;

    private Rigidbody2D GetRigidbody => m_playerInformation.GetRigidbody;

    private MotionInputData GetMotionInputData => m_playerInformation.GetMotionInputData;

    private CharacterProperty.PlayerMoveProperty GetMoveProperty => m_playerInformation.GetMoveProperty;

    private bool GetIsGround => m_playerInformation.GetIsGround;
    #endregion
    
    public override void Motion(BaseInformation information)
    {
        if (!CheckSuitableSlope)
        {
            GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            ChangeMotionState(MOTIONSTATEENUM.PlayerSlideState);
            return;
        }
        if (GetMotionInputData.MoveInput.x != 0)
        {
            GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            ChangeMotionState(MOTIONSTATEENUM.PlayerWalkAndRunState);
            return;
        }
        timmer += Time.fixedDeltaTime;
        if (timmer <= GetMoveProperty.GROUND_TIME_TO_STOP)
        {
            if (GetIsGround)
            {
                GetRigidbody.velocity = GetRigidbody.velocity.NewX(m_oriSpeed
                                                                   * (1-GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.GROUND_TIME_TO_STOP)));
            }
            else
            {
                GetRigidbody.velocity = GetRigidbody.velocity.NewX(m_oriSpeed
                                                                   * (1-GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.AIR_TIME_TO_STOP)));
            }
        }
        else
        {
            if (GetIsGround)
            {
                GetRigidbody.Freeze(FREEZEAXIS.PosXAndRotZ);
                return;
            }
            if (!CheckGlobalStates.Contains(typeof(PlayerJumpState)))
            {
                GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            }
        }
    }


    public PlayerMainDefultState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
        m_oriSpeed = GetRigidbody.velocity.x;
    }
}
}
