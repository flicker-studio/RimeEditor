using System;
using Moon.Transition;
using UnityEngine;

namespace Moon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Robot : MonoBehaviour
    {
        private RobotStateMachine m_stateMachine;
        internal Rigidbody2D rb2D { get; private set; }
        [SerializeField] public RobotConfig config;
        [SerializeField] internal RobotModel model;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            model = new RobotModel();
        }

        private void Start()
        {
            // StateMachine Init
            m_stateMachine = new RobotStateMachine(this);
            m_stateMachine.Add<PatrolState>();
            m_stateMachine.Add<SwitchFacingState>();

            m_stateMachine.AddTransition<WallTransition>();

            m_stateMachine.Start<PatrolState>();
        }

        private void Update()
        {
            m_stateMachine.OnUpdate();
            m_stateMachine.OnTransition();
        }

#if UNITY_EDITOR
        public bool enableDebug = true;

        private void OnGUI()
        {
            if (!enableDebug) return;
            //左上角绘制当前状态的状态
            GUI.Label(new Rect(0, 0, 100, 100), m_stateMachine.CurrentState.GetType().Name);
        }

        private void OnDrawGizmos()
        {
            if (config == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,
                transform.position + (Vector3)model.facingDir * config.wallCheckDistance);
        }
#endif
    }
}