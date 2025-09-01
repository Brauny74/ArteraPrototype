using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(SimplePool))]
    public class RangedWeapon : Weapon
    {
        /// Projectiles should be the Pool Object of the SimplePool

        [SerializeField]
        private Vector3 _shootPosition = Vector3.zero;

        private SimplePool _pool;

        private GameObject _shootPointObj;

        private void Start()
        {
            _pool = GetComponent<SimplePool>();
            if(_shootPointObj == null)
                CreateShootPoint();
        }

        public override void PerformAttack()
        {
            base.PerformAttack();
            var projectile = _pool.GetPooledObject();
            if (projectile != null)
            {
                projectile.transform.position = _shootPointObj.transform.position;
                projectile.transform.rotation = transform.rotation;
            }
        }

        public void CreateShootPoint()
        {
            _shootPointObj = new GameObject(this.gameObject.name + "ShootPoint");
            _shootPointObj.transform.position = transform.position + _shootPosition;
            _shootPointObj.transform.parent = transform;
        }

        private void OnDrawGizmosSelected()
        {
            if (_shootPointObj != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_shootPointObj.transform.position, 0.1f);
            }
        }
    }
}