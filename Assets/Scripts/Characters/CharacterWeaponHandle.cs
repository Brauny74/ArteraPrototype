using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    public class CharacterWeaponHandle : CharacterAbility
    {
        public int CurrentWeaponAmmo
        { 
            get
            {
                return _weaponInstance.currentMagazine;
            }
            set 
            {
                _weaponInstance.currentMagazine = value;
            }
        }

        public string CurrentWeaponName
        {
            get; private set;
        }

        [SerializeField]
        private WeaponAttachment weaponAttachment;

        [SerializeField]
        private Weapon startingWeaponPrefab;

        private Weapon _weaponInstance;

        public override void Initialize(Character character = null)
        {
            base.Initialize(character);
            SetWeapon(startingWeaponPrefab);
        }

        public void SetWeapon(string weaponString)
        {
            //TODO
        }

        public void SetWeapon(Weapon weaponPrefab)
        {
            RemoveCurrentWeapon();
            _weaponInstance = Instantiate(weaponPrefab) as Weapon;
            _weaponInstance.Initialize(this.transform);
            _weaponInstance.WeaponMesh.transform.parent = weaponAttachment.transform;
            _weaponInstance.WeaponMesh.transform.localPosition = Vector3.zero;
            _weaponInstance.WeaponMesh.transform.localRotation = Quaternion.identity;
            _animator.SetInteger("WeaponID", _weaponInstance.WeaponID);
            _weaponInstance.OnCycleEnd.AddListener(CycleEnd);
            CurrentWeaponName = weaponPrefab.name;
        }

        public void RemoveCurrentWeapon()
        {            
            if (_weaponInstance != null)
            {
                Destroy(_weaponInstance.gameObject);
            }
        }

        public override void PhysicsProcessUpdate()
        {
            
        }

        public override void ProcessUpdate()
        {
            

        }

        public override void Reset()
        {
            
        }

        public void Shoot()
        {
            if (_weaponInstance.Shoot())
            {
                _animator.SetBool("Idle", false);
                _animator.SetTrigger(_weaponInstance.AttackAnimationTrigger);
            }
        }

        private void CycleEnd()
        {
            _animator.SetBool("Idle", true);
        }
    }
}