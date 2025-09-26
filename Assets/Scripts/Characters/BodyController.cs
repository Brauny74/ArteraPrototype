using System;
using UnityEngine;

namespace TopDownShooter
{
    public class BodyController : MonoBehaviour
    {
        private Vector3 _moveVector = Vector3.zero;

        private Rigidbody _rb;
        private CharacterController _cc;

        private void Awake()
        {
            Reset();
            _rb = GetComponent<Rigidbody>();
            _cc = GetComponent<CharacterController>();
        }

        public void SetPosition(Vector3 newPos)
        {
            _cc.enabled = false;
            transform.position = newPos;
            _cc.enabled = true;
        }

        public void SetMovement(Vector3 moveVector)
        {
            Debug.Log("SetMovement");
            _moveVector = moveVector;
        }

        private void Update()
        {
            _cc.Move(_moveVector);
        }

        public void Reset()
        {
            _moveVector = Vector3.zero;
        }
    }
}