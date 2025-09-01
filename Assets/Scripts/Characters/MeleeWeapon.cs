using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField]
        private GameObject _damageZone;

        public override void Initialize(Transform parent)
        {
            base.Initialize(parent);
            _damageZone.SetActive(false);
        }

        public override void PerformAttack()
        {
            if (_damageZone != null)
            {
                _damageZone.SetActive(true);
            }
            base.PerformAttack();
        }

        protected override void AttackEnd()
        {
            if (_damageZone != null)
            {
                _damageZone.SetActive(false);
            }
        }
    }
}