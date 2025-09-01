using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField]
        private float _lifeTime;
        private float _currentLifeTime;
        private bool _immortal;

        [SerializeField]
        private float _velocity;

        [SerializeField]
        private Health _health;
        private Rigidbody rb;

        private void OnEnable()
        {
            _currentLifeTime = _lifeTime;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            _immortal = _lifeTime == 0;
            _health = GetComponent<Health>();
        }

        private void FixedUpdate()
        {
            rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * _velocity * TimeManager.Instance.TimeDilation);
        }

        private void Update()
        {
            if (_immortal || _health == null)
                return;
            _currentLifeTime -= Time.deltaTime;
            if (_currentLifeTime < 0)
                _health.Damage(_health.CurrentHealth + 1);
        }
    }
}