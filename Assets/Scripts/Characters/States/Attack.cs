using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;


namespace TopDownShooter
{
    public class Attack : State
    {
        [SerializeField] private float playerDistanceAttack;

        public override void Initialize(EnemyBrain _brain)
        {
            base.Initialize(_brain);
            currentStage = STAGE.ENTER;
        }

        public override void Enter()
        {
            base.Enter();
            brain.Agent.isStopped = true;
        }

        public override void Process()
        {
            if (!CanAttackPlayer())
            {
                nextState = STATE.IDLE;
                currentStage = STAGE.EXIT;
                return;
            }
            brain.Char.Rotation.SetLookDirection(GameManager.Instance.GetClosestPlayer(brain.transform.position).position);
            brain.Char.Rotation.SetMoveDirection(GameManager.Instance.GetClosestPlayer(brain.transform.position).position);
            brain.Char.WeaponHandle.Shoot();
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerDistancePursue);
        }
    }
}