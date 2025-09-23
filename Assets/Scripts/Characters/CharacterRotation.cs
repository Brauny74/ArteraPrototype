using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    public class CharacterRotation : CharacterAbility
    {
        Vector2 _moveDirection = Vector2.zero;
        Vector2 _lookDirection = Vector2.zero;

        public override void Initialize(Character character = null)
        {
            base.Initialize(character);
        }

        public override void PhysicsProcessUpdate()
        {
            if (!Allowed || TimeManager.Instance.IsPaused)
                return;
                        
            HandleRotation();
            HandleAnimation();
        }

        private void HandleRotation()
        {
            Vector3 turnDirection;
            turnDirection = transform.position + _lookDirection.ToVector3XZ();
            transform.LookAt(turnDirection);
        }

        private void HandleAnimation()
        {
            Vector2 normal;
            if (_moveDirection.sqrMagnitude < 0.0001f)
                normal = Vector2.zero;
            else
            {
                Vector2 forward = _lookDirection;
                Vector2 right = new Vector2(_lookDirection.y, -_lookDirection.x);
                float x = Vector2.Dot(_moveDirection, right);    // +1 = right, −1 = left
                float y = Vector2.Dot(_moveDirection, forward);
                normal = new Vector2(x, y);
            }

            _character.SetWalkDirection(normal);
        }

        public override void ProcessUpdate()
        {
            if (!Allowed)
                return;
        }

        public override void Reset()
        {
            _moveDirection = Vector3.zero;
            _lookDirection = Vector3.zero;
        }

        public void SetLookDirection(Vector3 lookDirection)
        {
            if (lookDirection != Vector3.zero)
            {
                _lookDirection = lookDirection.ToVector2XZ();
            }
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
                _moveDirection = moveDirection.ToVector2XZ();
        }

        
    }
}
