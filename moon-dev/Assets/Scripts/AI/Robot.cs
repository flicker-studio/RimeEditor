using System;
using UnityEngine;

namespace Moon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Robot : MonoBehaviour
    {
        private RobotStateMachine m_stateMachine;
        internal Rigidbody2D rb2D { get; private set; }
        internal MeshRenderer meshRenderer { get; private set; }
        [field: SerializeReference] internal TriggerEnter2D topTrigger { get; private set; } // 头顶碰撞盒 用于判断玩家是否踩到了机器人
        [SerializeField] internal RobotConfig config;
        [SerializeField] internal RobotModel model;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            meshRenderer = GetComponent<MeshRenderer>();


            model = new RobotModel();
        }

        private void Start()
        {
            // StateMachine Init
            m_stateMachine = new RobotStateMachine(this);
            // 所有的状态
            m_stateMachine.Add<PatrolState>();
            m_stateMachine.Add<SwitchFacingState>();
            m_stateMachine.Add<FlusteredState>();
            // 状态切换
            m_stateMachine.AddTransition<WallTransition>();
            // 默认状态
            m_stateMachine.Start<FlusteredState>();
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
            //列出所有可供切换的状态
            GUILayout.BeginArea(new Rect(0, 100, 100, 100));
            foreach (var state in m_stateMachine.GetAll())
            {
                if (GUILayout.Button(state.GetType().Name))
                {
                    m_stateMachine.Change(state.GetType());
                }
            }

            GUILayout.EndArea();
        }

        private void OnDrawGizmos()
        {
            if (config == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,
                transform.position + (Vector3)model.facingDir * config.facingCheckDistance);

            // //画出机器人的大小
            // Gizmos.color = Color.red;
            // Gizmos.DrawWireCube(transform.position, config.halfSize * 2);
        }
#endif
    }
}