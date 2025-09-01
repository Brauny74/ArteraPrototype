using TopDownShooter;
using Unity.VisualScripting;
using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(Usable))]
    public class WeaponPicker : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;
        private Usable usable;

        private void Start()
        {
            usable = GetComponent<Usable>();
            usable.OnUse.AddListener(OnUse);
        }

        public void OnUse()
        {
            var cwh = usable?.CurrentUser?.GetComponent<CharacterWeaponHandle>();
            cwh?.SetWeapon(weapon);
        }
    }
}