using UnityEngine;

namespace TopDownShooter
{
    public abstract class CharacterAbility : MonoBehaviour
    {
        public bool Allowed = true;

        protected Character _character;
        protected Animator _animator;

        public virtual void Initialize(Character character = null)
        {
            if (character == null)
                _character = GetComponent<Character>();
            else
                _character = character;
            _animator = _character.CharAnimator;
        }

        public abstract void ProcessUpdate();

        public abstract void PhysicsProcessUpdate();

        public abstract void Reset();
    }
}