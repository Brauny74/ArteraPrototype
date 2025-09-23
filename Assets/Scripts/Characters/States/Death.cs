using TopDownShooter;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    public class Death : State
    {
        public override void Initialize(EnemyBrain _brain)
        {
            base.Initialize(_brain);
            currentStage = STAGE.ENTER;
            state = STATE.DEATH;
        }

        public override void Enter()
        {
            base.Enter();
            brain.Agent.isStopped = true;
            brain.Char.StopWalk();
            brain.Char.SetDeath();
        }

        public override void Process()
        {
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}