using StateMachineTutorial;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter
{
    public class StateMachine : MonoBehaviour
    {
        Dictionary<State.STATE, State> states = new Dictionary<State.STATE, State>();

        State currentState;
        
        public EnemyBrain Brain;        

        private void Start()
        {
            states[State.STATE.IDLE] = GetComponent<Idle>();
            states[State.STATE.PATROL] = GetComponent<Patrol>();
            states[State.STATE.PURSUE] = GetComponent<Pursue>();
            states[State.STATE.ATTACK] = GetComponent<Attack>();
            currentState = states[State.STATE.IDLE];
            foreach (var state in states.Values)
            {
                state.StateChanged.AddListener(ProcessCurrentState);
            }
        }

        public void SwitchToState(State.STATE newState)
        {
            currentState = states[newState];
            currentState?.Initialize(Brain);
        }

        public void ProcessCurrentState()
        {
            currentState?.Process();
        }
    }
}