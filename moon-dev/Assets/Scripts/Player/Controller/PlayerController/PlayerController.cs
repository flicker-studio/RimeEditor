using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        private MotionController m_motionController;

        private PlayerInformation m_playerInformation;

        private UniTaskCompletionSource m_taskCompletionSource = new();

        private async UniTask ControllerInit()
        {
            var transform1 = transform;
            m_playerInformation = new PlayerInformation(transform1);
            await m_playerInformation.Init(transform1);
            m_motionController = new MotionController(m_playerInformation);
            m_taskCompletionSource.TrySetResult();
        }

        private void MotionInit()
        {
            m_motionController.ChangeMotionState(typeof(PlayerMainDefultState));
            m_motionController.ChangeMotionState(typeof(PlayerAdditiveDefultState));
            m_motionController.ChangeMotionState(typeof(PlayerPerpendicularGroundState));
        }

        private async void Start()
        {
            await ControllerInit();
            MotionInit();
        }

        private void FixedUpdate()
        {
            if (m_taskCompletionSource.Task.Status == UniTaskStatus.Succeeded)
            {
                m_motionController.Motion(m_playerInformation);
            }
        }

        private void OnDrawGizmos()
        {
            CharacterProperty temp = m_playerInformation.CharacterProperty;

            Gizmos.color = Color.green;

            Gizmos.DrawCube(transform.position + transform.up * temp.GroundCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y,
                temp.GroundCheckParameter.CHECK_CAPSULE_SIZE);

            Gizmos.color = Color.red;

            Gizmos.DrawCube(transform.position + transform.up * temp.CeilingCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y,
                temp.CeilingCheckParameter.CHECK_CAPSULE_SIZE);
        }
    }
}