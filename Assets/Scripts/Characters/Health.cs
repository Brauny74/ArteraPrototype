using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownShooter
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private int MaxHealth = 100;
        public int CurrentHealth
        {
            get; private set;
        } = 1;

        public UnityEvent OnDamage;
        public UnityEvent OnDeath;

        public enum DeathActions { Disable, Destroy };
        [SerializeField]
        private DeathActions _actionOnDeath;
        public DeathActions ActionOnDeath
        {
            get { return _actionOnDeath; }
        }

        public bool Alive { get; protected set; }

        /// <summary>
        /// Time while the object exists after death - usually to allow to play animation.
        /// </summary>
        [SerializeField]
        private float _deathTime;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
            Alive = true;
        }

        public void SetHealthDirectly(int health)
        {
            CurrentHealth = health;
            if (CurrentHealth < 0)
            {
                Die();
            }
        }

        public void Damage(int damage)
        {
            if (Alive)
            {
                CurrentHealth -= damage;
                OnDamage.Invoke();
                if (CurrentHealth < 0)
                {                    
                    Die();
                }
            }
        }

        private void Die()
        {
            Alive = false;
            OnDeath.Invoke();
            StartCoroutine("FinishDying");
        }

        private IEnumerator FinishDying()
        {
            yield return new WaitForSeconds(_deathTime);
            switch (_actionOnDeath)
            {
                case DeathActions.Disable:
                    this.gameObject.SetActive(false);
                    break;
                case DeathActions.Destroy:
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}