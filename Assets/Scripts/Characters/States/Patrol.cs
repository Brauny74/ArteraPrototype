using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace TopDownShooter
{

    public class Patrol : State
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float chanceToIdle;

        [SerializeField] private float patrolSpeed;
        [SerializeField] private Pathway patrolPath;

        private Transform currentDest;
        private float idleCooldown = 1.0f;
        private float currentIdleCooldown;

        public override void Initialize(EnemyBrain _brain)
        {
            if (patrolPath == null)
                Debug.LogError(brain.name + " has no Pathway assigned.");

            base.Initialize(_brain);
            currentStage = STAGE.ENTER;
            state = STATE.PATROL;
            currentIdleCooldown = idleCooldown;
        }

        public override void Enter()
        {
            base.Enter();
            brain.Char.StartWalk();
            SetNewDestination(patrolPath.GetClosestPoint(brain.transform.position));
            brain.Agent.speed = patrolSpeed;
            brain.Agent.isStopped = false;
        }

        public override void Process()
        {
            if(TimeManager.Instance.IsPaused)
            {
                brain.Agent.isStopped = true;
                return; 
            }else
            {
                brain.Agent.isStopped = false;
            }

            if (CanSeePlayer())
            {
                nextState = STATE.PURSUE;
                currentStage = STAGE.EXIT;
                return;
            }

            if (brain.Agent.remainingDistance < brain.Agent.stoppingDistance)
            {
                SetNewDestination(patrolPath.GetNextPoint(currentDest));
            }
            
            currentIdleCooldown -= Time.deltaTime;
            if (currentIdleCooldown < 0)
            {
                currentIdleCooldown = idleCooldown;
                if (Random.Range(0.0f, 1.0f) < chanceToIdle)
                {
                    nextState = STATE.IDLE;
                    currentStage = STAGE.EXIT;
                }
            }                        
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void SetNewDestination(Transform newDest)
        {
            currentDest = newDest;
            brain.Agent.SetDestination(currentDest.position);
            SetRotation(currentDest.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gold;
            Gizmos.DrawWireSphere(transform.position, playerDistancePursue);
        }
    }
}