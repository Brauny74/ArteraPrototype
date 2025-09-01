using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    public class PlayerBrain : MonoBehaviour
    {
        public UnityEvent OnPauseEvent;

        [SerializeField]
        private float _stickDeadZone = 0.02f;

        private string _inputScheme;
        private Vector2 _lookInput;
        private Vector2 mousePosition;
        private Ray ray;
        private Plane ground;

        private Character character;

        private void Awake()
        {
            ground = new Plane(Vector3.up, Vector3.zero);        
            character = GetComponent<Character>();
        }

        public void FixedUpdate()
        {
            HandleInput();
        }

        public void OnControlsChanged(PlayerInput pi)
        {
            _inputScheme = pi.currentControlScheme;
        }

        public void HandleInput()
        {
            switch (_inputScheme)
            {
                case "Gamepad":
                    Vector2 stick = _lookInput;
                    if (stick.sqrMagnitude > _stickDeadZone * _stickDeadZone)
                    {
                        character.Rotation.SetLookDirection(stick.normalized);
                    }
                    break;
                case "Keyboard&Mouse":
                    mousePosition = Mouse.current.position.value;
                    ray = Camera.main.ScreenPointToRay(mousePosition);                    
                    if (ground.Raycast(ray, out float enter))
                    {
                        Vector3 hit = ray.GetPoint(enter);
                        Vector3 direction = hit - transform.position;
                        if (direction.sqrMagnitude > 0.00001f)
                        {
                            character.Rotation.SetLookDirection(direction.normalized);
                        }
                    }
                    break;
            }
        }

        public void OnLook(InputValue value)
        {
            var inputVector = value.Get<Vector2>();
            OnLook(inputVector);
        }

        public void OnLook(Vector2 inputVector)
        {
            _lookInput = inputVector;
        }

        public void OnMove(InputValue value)
        {
            var inputVector = value.Get<Vector2>();
            character.Movement.SetMoveDirection(inputVector);
        }

        public void OnAttack(InputValue value)
        {
            character.WeaponHandle.Shoot();
        }

        public void OnInteract()
        {
            character.User.Use();
        }

        public void OnPause()
        {
            TimeManager.Instance.IsPaused = TimeManager.Instance.IsPaused ? false : true;
            OnPauseEvent?.Invoke();
        }
    }
}