using UnityEngine;

namespace TopDownShooter
{
    public class WeaponMesh : MonoBehaviour
    {
        [SerializeField]
        Vector3 positionOffset;
        [SerializeField]
        Vector3 rotationOffset;
        [SerializeField]
        bool ApplyOffset = true;

        //A better solution would be to have special VFX class that has a method to call all effects in any order
        //Would switch to it probably when the prototype is polished in other parts
        [SerializeField]
        ParticleSystem _muzzleFireEffect;

        public void Update()
        {
            if (!ApplyOffset)
                return;

            transform.localPosition = positionOffset;
            transform.localRotation = Quaternion.Euler(rotationOffset);
        }

        public void Shoot()
        {
            if(_muzzleFireEffect != null)
                _muzzleFireEffect.Play();
        }
    }
}