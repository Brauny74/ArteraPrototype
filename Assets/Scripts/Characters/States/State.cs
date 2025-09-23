using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace TopDownShooter
{

    public abstract class State : MonoBehaviour
    {
        [SerializeField] protected LayerMask obstacleLayerMask;
        [SerializeField] protected float playerDistancePursue;

        public UnityEvent<STATE> StateChanged;

        public enum STATE
        {
            IDLE, PATROL, PURSUE, ATTACK, DEATH
        };

        public enum STAGE
        {
            ENTER, UPDATE, EXIT
        };

        public STATE state;
        protected STAGE currentStage;

        protected EnemyBrain brain;
        protected STATE nextState;

        public virtual void Initialize(EnemyBrain _brain)
        {
            currentStage = STAGE.ENTER;
            brain = _brain;
        }

        public virtual void Enter()
        { 
            DebugUI.Instance.EnemyState = state;
            currentStage = STAGE.UPDATE;
        }

        public virtual void Process() { currentStage = STAGE.UPDATE; }

        public virtual void Exit() { 
            currentStage = STAGE.EXIT;
            StateChanged?.Invoke(nextState);
        }

        public virtual STATE ProcessStages()
        {
            switch (currentStage)
            {
                case STAGE.ENTER:
                    Enter();
                    break;
                case STAGE.UPDATE:
                    Process();
                    break;
                case STAGE.EXIT:
                    Exit();
                    return nextState;
            }
            return this.state;
        }

        protected void SetRotation(Vector3 movToPos)
        {
            brain.Char.SetWalkDirection(Vector2.up);
        }

        protected bool CanSeePlayer()
        {
            Vector3 direction = brain.transform.position -
                GameManager.Instance.GetClosestPlayer(brain.transform.position).position;
            if (direction.magnitude < playerDistancePursue)
            {
                if (!Physics.Raycast(brain.transform.position, direction, playerDistancePursue, obstacleLayerMask))
                {
                    return true;
                }
            }
            return false;
        }

    }
}