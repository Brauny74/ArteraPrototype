using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    public class Pursue : State
    {
        [SerializeField] private float playerDistanceAttack;
        [SerializeField] private float pursueSpeed;

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
            //Good enough for single player, needs tweaking for multiplayer
            brain.Agent.SetDestination(GameManager.Instance.GetClosestPlayer(brain.transform.position).position);
            brain.Agent.isStopped = false;
        }

        public override void Process()
        {
            brain.Char.Rotation.SetLookDirection(GameManager.Instance.GetClosestPlayer(brain.transform.position).position);
            brain.Char.Rotation.SetMoveDirection(GameManager.Instance.GetClosestPlayer(brain.transform.position).position);
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
                GameManager.Instance.GetClosestPlayer(brain.transform.position).position;
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
            Gizmos.color = Color.yellowGreen;
            Gizmos.DrawWireSphere(transform.position, playerDistancePursue);
            Gizmos.color = Color.darkRed;
            Gizmos.DrawWireSphere(transform.position, playerDistanceAttack);
        }
    }
}