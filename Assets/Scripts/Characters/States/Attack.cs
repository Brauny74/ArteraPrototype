using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;


namespace TopDownShooter
{
    public class Attack : State
    {
        [SerializeField] private float playerDistanceAttack;
        private Transform playerTransform;

        public override void Initialize(EnemyBrain _brain)
        {
            base.Initialize(_brain);
            currentStage = STAGE.ENTER;
        }

        public override void Enter()
        {
            base.Enter();
            brain.Char.StopWalk();
            brain.Agent.isStopped = true;
            playerTransform = GameManager.Instance.GetClosestPlayer(brain.transform.position);
        }

        public override void Process()
        {
            if (TimeManager.Instance.IsPaused)
            { return; }

            brain.Char.transform.LookAt(playerTransform.position);
            if (!CanAttackPlayer())
            {
                nextState = STATE.IDLE;
                currentStage = STAGE.EXIT;
                return;
            }
            SetRotation(GameManager.Instance.GetClosestPlayer(brain.transform.position).position);
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
            Gizmos.DrawWireSphere(transform.position, playerDistanceAttack);
        }
    }
}