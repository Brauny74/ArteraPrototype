using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{ 
    public class Idle : State
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float chanceToPatrol;

        public override void Initialize(EnemyBrain _brain)
        {
            base.Initialize(_brain);
            currentStage = STAGE.ENTER;
            state = STATE.IDLE;
        }
        
        public override void Enter()
        {
            base.Enter();
            brain.Agent.isStopped = true;
            SetRotation(Random.rotation.eulerAngles);
            brain.Char.StopWalk();
        }

        public override void Process()
        {
            if (CanSeePlayer())
            {
                nextState = STATE.PURSUE;
                currentStage = STAGE.EXIT;
            }else if (Random.Range(0.0f, 1.0f) < chanceToPatrol)
            {
                nextState = STATE.PATROL;
                currentStage = STAGE.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, playerDistancePursue);
        }
    }
}