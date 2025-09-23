using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    public class Pursue : State
    {
        [SerializeField] private float playerDistanceAttack;
        [SerializeField] private float pursueSpeed;

        Transform playerTransform;

        public override void Initialize(EnemyBrain _brain)
        {
            base.Initialize(_brain);
            currentStage = STAGE.ENTER;
        }

        public override void Enter()
        {
            base.Enter();
            brain.Char.StartRun();
            brain.Agent.speed = pursueSpeed;
            playerTransform = GameManager.Instance.GetClosestPlayer(brain.transform.position);
            brain.Agent.SetDestination(playerTransform.position);
            brain.Agent.isStopped = false;
        }

        public override void Process()
        {
            SetRotation(playerTransform.position);
            brain.Agent.SetDestination(playerTransform.position);
            if (CanAttackPlayer())
            {
                nextState = STATE.ATTACK;
                currentStage = STAGE.EXIT;
            } else if (!CanSeePlayer())
            {
                nextState = STATE.IDLE;
                currentStage = STAGE.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        private bool CanAttackPlayer()
        {
            Vector3 direction = brain.transform.position -
                playerTransform.position;
            if (direction.magnitude < playerDistanceAttack)
            {
                if (!Physics.Raycast(brain.transform.position, direction, playerDistanceAttack, obstacleLayerMask))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, playerDistancePursue);
            Gizmos.color = Color.pink;
            Gizmos.DrawWireSphere(transform.position, playerDistanceAttack);
        }
    }
}