using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Attributes
{
    [System.Serializable]
    public struct HealthEvents
    {
        public UnityEvent OnDeath;
        public UnityEvent<float> OnDamage;
        public UnityEvent OnCure;
        public UnityEvent<float> OnHeal;
        public UnityEvent<float> OnHealthChanged;
    }

    public class HealthAttribute : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField]
        [Tooltip("The maximum health of the entity")]
        private float maxHealth = 100;
        private float health = 100;

        [SerializeField]
        [Tooltip("Health regeneration per second")]
        private float healthRegen = 0;

        [SerializeField]
        private Resistance resistances;

        [Header("Callback Events")]
        [Tooltip(
            "Attribute Events\n\n"
                + "- OnDeath: Called when the entity's health reaches 0\n\n"
                + "- OnDamage: Called when the entity takes damage and passes the damage amount\n\n"
                + "- OnCure: Called when the entity is cured\n\n"
                + "- OnHeal: Called when the entity is healed and passes the amount to heal\n\n"
                + "- OnHealthChanged: Called when the entity's health changes and passes the percentage of health left"
        )]
        [SerializeField]
        public HealthEvents events;

        public float Health
        {
            get { return health; }
            private set
            {
                if (health == value)
                    return;

                health = value;
                events.OnHealthChanged?.Invoke(health / maxHealth);

                if (health <= 0)
                {
                    events.OnDeath?.Invoke();
                }
                else if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
        }

        private void OnValidate()
        {
            Health = maxHealth;
        }

        public void TakeDamage(float damage, EDamageType type)
        {
            Health -= damage;

            events.OnDamage?.Invoke(damage);
        }

        public void Cure(EDamageType type)
        {
            events.OnCure?.Invoke();
        }

        public void Heal(float amount)
        {
            Health += amount;
            events.OnHeal?.Invoke(amount);
        }

        public void Update()
        {
            Health += healthRegen * Time.deltaTime;

            TakeDamage(10f * Time.deltaTime, EDamageType.Physical);
        }
    }
}
