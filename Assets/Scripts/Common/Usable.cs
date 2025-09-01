using UnityEngine;
using UnityEngine.Events;

namespace TopDownShooter
{
    public class Usable : MonoBehaviour, ISaveable
    {
        [SerializeField] private string id;
        [ContextMenu("Create GUID")]
        private void CreateGuid()
        {
            id = System.Guid.NewGuid().ToString();
        }

        public UnityEvent OnUse;
        
        public CharacterUser CurrentUser {  get; private set; }
        [SerializeField]
        private GameObject _inWorldUI;

        private void OnEnable()
        {
            _inWorldUI?.SetActive(false);
        }

        public void Use()
        {
            OnUse.Invoke();
            this.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Uninit();
        }

        private void OnDisable()
        {
            Uninit();
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterUser characterUser = other.GetComponent<CharacterUser>();
            if (characterUser != null)
            {
                _inWorldUI.SetActive(true);
                CurrentUser = characterUser;
                CurrentUser.SetUsable(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == CurrentUser?.gameObject)
            {
                Uninit();
            }
        }

        private void Uninit()
        {
            CurrentUser?.UnsetUsable(this);
            _inWorldUI?.SetActive(false);
        }

        public void Save(ref GameData gameData)
        {
            gameData.Usables[id] = gameObject.activeInHierarchy;
        }

        public void Load(GameData gameData)
        {
            if (!gameData.Usables.ContainsKey(id))
                gameData.Usables[id] = true;
            this.gameObject.SetActive(gameData.Usables[id]);
        }
    }
}