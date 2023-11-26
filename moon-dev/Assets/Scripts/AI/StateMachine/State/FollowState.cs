using UnityEngine;
using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 跟随状态 -> 跟随目标进行移动和跳跃
    /// </summary>
    internal class FollowState : State<Robot>
    {
        private bool m_canFollow = true;
        private Timer m_timer;


        public override void OnUpdate(Robot owner)
        {
            // 在播放转向动画
            if (m_timer != null)
            {
                return;
            }

            //先朝向目标
            var tarFacing = owner.followTarget.position.x > owner.transform.position.x
                ? FacingDirection2D.Right
                : FacingDirection2D.Left;
            
            // 无需转向
            if (owner.model.facing == tarFacing)
            {
                m_canFollow = true;
            }
            else
            {
                m_canFollow = false;
                m_timer = Timer.Register(1, () =>
                {
                    owner.model.facing = tarFacing;
                    m_canFollow = true;
                    m_timer = null;
                });
            }
            
            if (!m_canFollow) return;

            Vector3 ownerPosition = owner.transform.position;
            Vector3 targetPosition = owner.followTarget.position;


            float xDistance = Mathf.Abs(targetPosition.x - ownerPosition.x);
            float yDistance = Mathf.Abs(targetPosition.y - ownerPosition.y);

            Vector3 direction = targetPosition - ownerPosition;
            if (xDistance > owner.config.followDistance.x)
            {
                Vector3 xDirection = new Vector3(direction.x, 0, 0);
                owner.transform.Translate(xDirection.normalized * (owner.config.followSpeed * Time.deltaTime));
            }

            // TODO  跳跃
            // if(yDistance > owner.config.followDistance.y)
            // {
            //     Vector3 yDirection = new Vector3(0, direction.y, 0);
            //     owner.transform.Translate(yDirection.normalized * (owner.config.followSpeed * Time.deltaTime));
            // }
        }
    }
}