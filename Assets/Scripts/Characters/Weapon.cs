using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

namespace TopDownShooter
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        private int weaponID;

        public int WeaponID
        {
            get {  return weaponID; }
        }

        [SerializeField]
        private int _maxMagazine;
        [SerializeField]
        private WeaponMesh _weaponMeshPrefab;

        public WeaponMesh WeaponMesh
        { get; private set; }

        public int currentMagazine;

        [SerializeField]
        protected float _windUpTime;
        [SerializeField]
        protected float _attackTime;
        [SerializeField]
        protected float _windDownTime;

        public string AttackAnimationTrigger = "Attack";

        public UnityEvent OnCycleEnd; 

        protected enum WeaponState { Idle, WindUp, Shooting, WindDown }
        protected WeaponState currentWeaponState;

        public virtual void Initialize(Transform parent)
        {
            this.transform.parent = parent;
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;

            currentMagazine = _maxMagazine;
            currentWeaponState = WeaponState.Idle;

            WeaponMesh = Instantiate(_weaponMeshPrefab);
        }

        /// <summary>
        /// Called to actually shoot the weapon. It's a primitive state machine, so shouldn't be overridden.
        /// To override attack, override PerformAttack()
        /// It's primitive, because only one state really does something.
        /// </summary>
        /// <returns>true is shot successfuly, false otherwise (because of cooldown)</returns>
        public virtual bool Shoot()
        {
            switch(currentWeaponState)
            {
                case WeaponState.Idle:
                    if (_maxMagazine > 0)
                        currentMagazine--;
                    currentWeaponState = WeaponState.WindUp;
                    StartCoroutine("WindUp");
                    return true;
                case WeaponState.WindUp:
                case WeaponState.Shooting:
                case WeaponState.WindDown:
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Where the actual attack is performed. Should be overridden in other types of Weapon
        /// </summary>
        public virtual void PerformAttack()
        {
            currentWeaponState = WeaponState.Shooting;
            WeaponMesh.Shoot();
            StartCoroutine("Shooting");
        }
        
        public virtual void DropWeapon()
        { }

        protected virtual IEnumerator WindUp()
        {
            yield return new WaitForSeconds(_windUpTime);            
            PerformAttack();
        }

        protected virtual IEnumerator Shooting()
        {
            yield return new WaitForSeconds(_attackTime);
            currentWeaponState = WeaponState.WindDown;
            StartCoroutine("WindDown");
        }

        protected virtual IEnumerator WindDown()
        {
            yield return new WaitForSeconds(_windDownTime);
            AttackEnd();
            OnCycleEnd.Invoke();
            currentWeaponState = WeaponState.Idle;
            if (_maxMagazine > 0)
            {
                if(currentMagazine <= 0)
                    DropWeapon();
            }
        }

        protected virtual void AttackEnd()
        {}

        private void OnDestroy()
        {
            Destroy(WeaponMesh.gameObject);
        }

    }
}