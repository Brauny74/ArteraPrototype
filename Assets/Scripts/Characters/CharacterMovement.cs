using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    public class CharacterMovement : CharacterAbility
    {
        [SerializeField]
        private float maxMovementSpeed = 1.0f;
        private float movementSpeed = 0.0f;
        [SerializeField]
        private float acceleration = 0.2f;
        [SerializeField]
        private float deceleration = 0.2f;

        Vector3 _direction;

        CharacterRotation _charRotation;

        public override void Initialize(Character character = null)
        {
            base.Initialize(character);
            _charRotation = this.gameObject.GetComponent<CharacterRotation>();
        }

        public override void PhysicsProcessUpdate()
        {
            if (!Allowed || TimeManager.Instance.IsPaused)
                return;
            HandleMovement();
            HandleRotation();
            HandleAnimation();
        }

        public override void ProcessUpdate()
        {
            if (!Allowed)
                return;
        }

        public override void Reset()
        {
            _character.StopCharacter();
        }

        private void HandleRotation()
        {
            if (_charRotation == null)
                return;

            _charRotation.SetMoveDirection(_direction);
        }

        private void HandleAnimation()
        {
            if (movementSpeed > 0.0f)
            {
                _character.StartWalk();
            }
            else
            {
                _character.StopWalk();
            }
        }

        private void HandleMovement()
        {
            if (_direction == Vector3.zero)
            {
                movementSpeed -= deceleration * Time.fixedDeltaTime;
                if (movementSpeed < 0.0f)
                    movementSpeed = 0.0f;
            }
            else
            {
                if (movementSpeed < maxMovementSpeed)
                {
                    movementSpeed += acceleration * Time.fixedDeltaTime;
                }
            }
            Debug.Log("HandleMovement");
             _character.BodyController.SetMovement(_direction * movementSpeed * Time.fixedDeltaTime);
        }

        public void SetMoveDirection(Vector2 inputVector)
        {
            if (inputVector.magnitude != 0.0)
                _direction = new Vector3(inputVector.x, 0.0f, inputVector.y);
            else _direction = Vector3.zero;
        }
    }
}