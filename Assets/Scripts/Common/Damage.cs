using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(Collider))]
    public class Damage : MonoBehaviour
    {
        [SerializeField]
        private int DamageValue;

        [SerializeField]
        private int SelfDamage;

        [SerializeField]
        private Health _health;

        private List<Collider> touchedColliders;

        private void Start()
        {
            if(_health == null)
                _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (touchedColliders != null)
                touchedColliders.Clear();
            else 
                touchedColliders = new List<Collider>();
        }

        /*private void OnTriggerEnter(Collider other)
        {
            var otherHealth = other.gameObject.GetComponent<Health>();
            if (otherHealth != null)
            {
                otherHealth.Damage(DamageValue);
                if(_health != null)
                    _health.Damage(SelfDamage);
            }
        }*/

        private void OnTriggerStay(Collider other)
        {
            if (!touchedColliders.Contains(other))
            {
                var otherHealth = other.gameObject.GetComponent<Health>();
                if (otherHealth != null)
                {
                    otherHealth.Damage(DamageValue);                    
                }
                if (_health != null)
                    _health.Damage(SelfDamage);
                touchedColliders.Add(other);
            }
        }
    }
}