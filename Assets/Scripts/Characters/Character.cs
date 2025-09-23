using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

namespace TopDownShooter
{
    public class Character : MonoBehaviour, ISaveable
    {
        [SerializeField] private string id;
        [ContextMenu("Create GUID")]
        private void CreateGuid()
        {
            id = System.Guid.NewGuid().ToString();
        }
        public string Id { get { return id; } }

        [SerializeField]
        public WeaponAttachment _weaponAttachment
        { get; private set; }

        public Animator CharAnimator
        {
            get { return _animator; }
        }

        public BodyController BodyController
        {
            get { return _bodyContorller; }
        }

        [SerializeField]
        private Animator _animator;

        private BodyController _bodyContorller;

        private CharacterAbility[] _abilities;

        public CharacterMovement Movement;
        public CharacterRotation Rotation;
        public CharacterWeaponHandle WeaponHandle;
        public CharacterUser User;
        public Health Health;

        public Action<string> OnDeath;

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            if(_bodyContorller == null)
                _bodyContorller = GetComponent<BodyController>();
            if (Health == null)
                Health = GetComponent<Health>();
            _abilities = GetComponents<CharacterAbility>();
            foreach (CharacterAbility ability in _abilities)
            {
                ability.Initialize(this);
            }
            _weaponAttachment = GetComponentInChildren<WeaponAttachment>();
            if (Health != null)
            {
                Health.OnDeath.AddListener(SetDeath);
                Health.OnDeath.AddListener(Die);
            }

            WeaponHandle = GetComponent<CharacterWeaponHandle>();
            Movement = GetComponent<CharacterMovement>();
            Rotation = GetComponent<CharacterRotation>();
            User = GetComponent<CharacterUser>();
        }

        void Update()
        {
            foreach (CharacterAbility ability in _abilities)
            {
                ability.ProcessUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (CharacterAbility ability in _abilities)
            {
                ability.PhysicsProcessUpdate();
            }
        }

        public void StopCharacter()
        {
            _bodyContorller.Reset();
        }

        public void StartWalk()
        {
            if (_animator == null)
                return;
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walking", true);
            _animator.SetBool("Running", false);
        }

        public void StopWalk()
        {
            if (_animator == null)
                return;
            _animator.SetBool("Idle", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
        }

        public void StartRun()
        {
            if (_animator == null)
                return;
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", true);
        }

        public void StopRun()
        {
            if (_animator == null)
                return;
            _animator.SetBool("Walking", true);
            _animator.SetBool("Running", false);
        }

        public void SetWalkDirection(Vector2 direction)
        {
            if (_animator == null)
                return;
            _animator.SetFloat("NormalX", direction.x);
            _animator.SetFloat("NormalY", direction.y);
        }

        public void SetDeath()
        {
            if (_animator == null)
                return;
            _animator.applyRootMotion = true;
            _animator.SetBool("Alive", false);
        }

        public void Die()
        {
            OnDeath?.Invoke(id);
        }

        public void Save(ref GameData gameData)
        {
            if (!gameData.Characters.ContainsKey(id))
            {
                gameData.Characters.Add(id, new CharacterData());
            }
            if (WeaponHandle != null)
            {
                gameData.Characters[id].WeaponName = WeaponHandle.CurrentWeaponName;
                gameData.Characters[id].CurrentWeaponAmmo = WeaponHandle.CurrentWeaponAmmo;
            }
            if(Health != null)
            {
                gameData.Characters[id].CurrentHealth = Health.CurrentHealth;
            }
            gameData.Characters[id].Position = transform.position;
            gameData.Characters[id].Rotation = transform.rotation;            
        }

        public void Load(GameData gameData)
        {
            if (gameData.Characters[id].IsDead)
            {
                switch(Health.ActionOnDeath)
                {
                    case Health.DeathActions.Disable:
                        gameObject.SetActive(false); 
                        break;
                    case Health.DeathActions.Destroy: 
                        Destroy(this.gameObject); 
                        break;
                }
                return;
            }

            if (Health != null)
                Health.SetHealthDirectly(gameData.Characters[id].CurrentHealth);

            if (WeaponHandle != null)
            {
                WeaponHandle.SetWeapon(gameData.Characters[id].WeaponName);
                WeaponHandle.CurrentWeaponAmmo = gameData.Characters[id].CurrentWeaponAmmo;
            }

            transform.position = gameData.Characters[id].Position;
            transform.rotation = gameData.Characters[id].Rotation;
        }
    }
}