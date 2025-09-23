using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class EnemyStateMachine : MonoBehaviour
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
            states[State.STATE.DEATH] = GetComponent<Death>();
            currentState = states[State.STATE.IDLE];
            currentState.Initialize(Brain);
            foreach (var state in states.Values)
            {
                state.StateChanged.AddListener(SwitchToState);
            }
            Brain.Char.Health.OnDeath.AddListener(SwitchToDeathState);
        }

        public void SwitchToDeathState()
        {
            currentState = states[State.STATE.DEATH];
            currentState?.Initialize(Brain);
        }

        public void SwitchToState(State.STATE newState)
        {
            currentState = states[newState];
            currentState?.Initialize(Brain);
        }

        public void ProcessCurrentState()
        {
            currentState?.ProcessStages();
        }
    }
}