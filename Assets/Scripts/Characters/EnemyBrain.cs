using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    public class EnemyBrain : MonoBehaviour
    {
        
        [SerializeField]
        private NavMeshAgent agent;
        public NavMeshAgent Agent
            { get { return agent; } }

        [Tooltip("An object containing State components, if null, defaults to this objecft.")]
        [SerializeField]
        private EnemyStateMachine stateMachine;

        [SerializeField] private Transform mesh;

        Character character;
        public Character Char
        {  get { return character; } }

        private void Awake()
        {
            if(agent == null)
                agent = GetComponent<NavMeshAgent>();
            character = agent.GetComponent<Character>();
            stateMachine.Brain = this;
        }

        private void Update()
        {
            if(mesh != null)
            {
                mesh.localPosition = Vector3.zero;
            }
            stateMachine.ProcessCurrentState();
        }

        //private void Start()
        //{
        //    if(stateMachine == null)
        //        stateMachine = GetComponent<StateMachine>();
        //    stateMachine.Brain = this;
        //    if (statesObject == null) statesObject = this.gameObject;
        //    agent = this.GetComponent<NavMeshAgent>();
        //    character = this.GetComponent<Character>();
        //    currentState = new Idle(this.gameObject, agent, animator);
        //}

        //private void Update()
        //{
        //    currentState = currentState.Process();
        //}
    }
}