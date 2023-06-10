using UnityEngine;

namespace Scripts.Attributes
{
    public interface IHealth
    {
        void TakeDamage(float damage, EDamageType type);
        void Cure(EDamageType type);
        void Heal(float amount);
        void Die();
    }
}
