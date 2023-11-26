using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Moon
{
    // [RequireComponent(typeof(Rigidbody2D))]
    public class Robot : MonoBehaviour
    {
        private RobotStateMachine m_stateMachine;
        
        internal MeshRenderer meshRenderer { get; private set; }
        [field: SerializeReference] internal TriggerEnter2D headTrigger { get; private set; } // 头顶碰撞盒 用于判断玩家是否踩到了机器人
        
        [field: SerializeField] internal Animator animator { get; private set; }
        [SerializeField] internal RobotConfig config;
        [SerializeField] internal Rigidbody2D rb2D; // 被射击时，用来模拟运动的刚体
        [SerializeField] internal RobotModel model;


        internal Transform followTarget;
        private Transform m_collectorTransform; // 收纳状态下 收纳者的Transform


        #region InitMethods

        private void _InitStateMachine()
        {
            m_stateMachine = new RobotStateMachine(this);
            // 所有的状态
            m_stateMachine.Add<PatrolState>();
            m_stateMachine.Add<FlusteredState>();
            m_stateMachine.Add<FollowState>();
            m_stateMachine.Add<IdleState>();
            m_stateMachine.Add<BeCollectedState>();

            m_stateMachine.Add<SwitchFacingState>();
            m_stateMachine.Add<HitPlayerState>();
            m_stateMachine.Add<BeActiveState>();
            m_stateMachine.Add<BeTrampledState>();
            m_stateMachine.Add<BeShotState>();

            // 状态切换
            m_stateMachine.AddTransition<MainTransition>();
            // 默认状态
            m_stateMachine.Start<FlusteredState>();
        }

        #endregion

        private void Awake()
        {
            // rb2D = GetComponent<Rigidbody2D>();
            meshRenderer = GetComponent<MeshRenderer>();


            model = new RobotModel();
        }

        private void Start()
        {
            // StateMachine Init
            _InitStateMachine();
        }


        private void Update()
        {
            m_stateMachine.OnUpdate();
            m_stateMachine.OnTransition();
        }

        public void SetFollowTarget(Transform target)
        {
            followTarget = target;
            m_stateMachine.Change<FollowState>();
        }

        //被收纳
        public void BeCollected(Transform collector)
        {
            m_collectorTransform = collector;
            m_stateMachine.Change<BeCollectedState>();
        }

        public void BeShot(Vector3 pos, Vector2 force)
        {
            transform.position = pos;
            m_stateMachine.Change<BeShotState>();
            rb2D.AddForce(force, ForceMode2D.Impulse);
        }


        /// <summary>
        /// TODO 将拍摄的数据存储到Robot里
        /// </summary>
        public void StoreData(object data)
        {
            throw new NotImplementedException();
        }

#if UNITY_EDITOR
        [Header("Debug")] public bool enableDebug = false;
        public Transform debugTarget;
        public Transform debugCollector;
        public Vector2 debugShotForce = Vector2.one * 7;

        private void OnGUI()
        {
            if (!enableDebug) return;

            GUILayout.BeginVertical();
            GUILayout.Label($"State:{m_stateMachine.CurrentState.GetType().Name}");
            foreach (var state in m_stateMachine.GetAll())
            {
                if(state.GetType() == typeof(FollowState))continue;
                if(state.GetType() == typeof(BeCollectedState))continue;
                if(state.GetType() == typeof(BeShotState))continue;
                // Debug.Log(state.GetType().Name);
                if (GUILayout.Button(state.GetType().Name))
                {
                    m_stateMachine.Change(state.GetType());
                }
            }

            // 发射按钮
            GUILayout.BeginHorizontal();
            Vector2 flyForce = UnityEditor.EditorGUILayout.Vector2Field("FlyForce", debugShotForce);
            if (GUILayout.Button("Shot"))
            {
                BeShot(transform.position, flyForce);
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("BeCollected") && debugCollector != null)
            {
                BeCollected(debugCollector);
            }

            if (GUILayout.Button("Follow") && debugTarget != null)
            {
                SetFollowTarget(debugTarget);
            }

            GUILayout.EndVertical();
        }

        private void OnValidate()
        {
            Vector3 position = transform.position;
            if (headTrigger != null)
            {
                headTrigger.transform.position = position;
            }
        }

        private void OnDrawGizmos()
        {
            if (config == null) return;

            var position = transform.position;
            Gizmos.color = Color.green;

            // 画出面朝方向碰撞检测的线
            Gizmos.DrawLine(position,
                position + (Vector3)model.facingDir * config.facingCheckDistance);

            // 画出地面检测的线
            Gizmos.DrawLine(position,
                position + Vector3.down * config.groundCheckDistance);

            // 画出踩头的最小限制点
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position + Vector3.up * config.topOffsetY, 0.1f);


            Gizmos.color = Color.yellow;

            // 画出跟随状态下的跟随目标
            if (enableDebug && debugTarget != null)
            {
                Gizmos.DrawSphere(debugTarget.position, 0.1f);
            }
            
            // 画出跟随的最小距离圈
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, config.followDistance.x);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, config.followDistance.y);
        }
#endif
    }
}