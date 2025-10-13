using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TopDownShooter
{
    public class CharacterWeaponHandle : CharacterAbility
    {
        private int _currentWeaponAmmo;
        public int CurrentWeaponAmmo
        { 
            get
            {
                if (_weaponInstance == null)
                    return 10;
                return _weaponInstance.currentMagazine;
            }
            set 
            {
                _currentWeaponAmmo = value;
            }
        }

        public string CurrentWeaponName
        {
            get; private set;
        }

        private bool _canShoot;

        [SerializeField]
        private WeaponAttachment weaponAttachment;

        //[SerializeField]
        //private Weapon startingWeaponPrefab;

        [SerializeField]
        private AssetReferenceGameObject startingWeaponReference;

        [SerializeField]
        [Tooltip("The weapon that the player picks, when dropping the main weapon.")]
        ///The weapon that the player picks, when dropping the main weapon.
        private AssetReferenceGameObject defaultWeapon;

        private Weapon _weaponInstance;

        private AsyncOperationHandle<GameObject> weaponRefHandle;

        public override void Initialize(Character character = null)
        {
            base.Initialize(character);            
            SetWeapon(startingWeaponReference);
        }

        public void SetWeapon(string weaponString)
        {
            _canShoot = false;
            weaponRefHandle = Addressables.LoadAssetAsync<GameObject>(weaponString);
            weaponRefHandle.Completed += OnWeaponLoadCompleted;
        }

        public void SetWeapon(AssetReferenceGameObject weaponRef)
        {
            if (!weaponRef.RuntimeKeyIsValid())
            {
                Debug.LogError($"Ref Eror: Weapon not loaded: {weaponRef}");
                return;
            }

            _canShoot = false;
            weaponRefHandle = Addressables.LoadAssetAsync<GameObject>(weaponRef);
            weaponRefHandle.Completed += OnWeaponLoadCompleted;
        }

        private void OnWeaponLoadCompleted(AsyncOperationHandle<GameObject> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                SetWeapon(asyncOperationHandle.Result.GetComponent<Weapon>());
                if(_currentWeaponAmmo != 0)
                    _weaponInstance.currentMagazine = _currentWeaponAmmo;
                _canShoot = true;
            }
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
            _weaponInstance.OnEmptyMagazine.AddListener(SetDefaultWeapon);
        }

        private void SetDefaultWeapon()
        {
            SetWeapon(defaultWeapon);
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
            if (Allowed && _canShoot && _weaponInstance.Shoot())
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