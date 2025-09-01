using UnityEngine;

namespace TopDownShooter
{
    public class CharacterUser : CharacterAbility
    {
        private Usable _currentUsable;

        public void SetUsable(Usable usable)
        {
            _currentUsable = usable;
        }

        public void UnsetUsable(Usable usable)
        {
            if(usable == _currentUsable)
                _currentUsable = null;
        }

        public void Use()
        {
            if (_currentUsable == null)
                return;

            _currentUsable.Use();
        }

        public override void PhysicsProcessUpdate()
        {
           
        }

        public override void ProcessUpdate()
        {
           
        }

        public override void Reset()
        {
           
        }
    }
}