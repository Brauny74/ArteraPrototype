using System;
using UnityEngine;

namespace TopDownShooter
{
    public class BodyController : MonoBehaviour
    {
        private Vector3 _moveVector = Vector3.zero;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void SetMovement(Vector3 moveVector)
        {
            _moveVector = moveVector;
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(transform.position + _moveVector);
        }

        public void Reset()
        {
            _moveVector = Vector3.zero;
        }
    }
}