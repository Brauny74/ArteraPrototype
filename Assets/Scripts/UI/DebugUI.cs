using UnityEngine;
using TMPro;

namespace TopDownShooter
{
    public class DebugUI : Singleton<DebugUI>
    {
        private State.STATE _state;
        public State.STATE EnemyState
        {
            get
                { return _state; }
            set
            {
                _state = value;
                Text1.text = StateToString(value);
            }
        }

        private Vector3 _goal;
        public Vector3 Goal
        {
            get { return _goal; }
            set
            {
                _goal = value;
                Text2.text = "Patrol/Pursue goal: " + value.ToString();
            }
        }

        public TMP_Text Text1;
        public TMP_Text Text2;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Text1.text = "";
        }

        string StateToString(State.STATE state)
        {
            switch (state)
            {
                case State.STATE.IDLE:
                    return "Idle";
                case State.STATE.PATROL:
                    return "Patrol";
                case State.STATE.PURSUE:
                    return "Pursue";
                case State.STATE.ATTACK:
                    return "Attack";
                case State.STATE.DEATH:
                    return "Death";
            }
            return "";
        }
    }
}