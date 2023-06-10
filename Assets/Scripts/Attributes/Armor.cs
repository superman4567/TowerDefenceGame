using UnityEngine;

namespace Scripts.Attributes
{
    public class Armor : MonoBehaviour, IArmor
    {
        [Header("Resistance Settings")]
        [SerializeField]
        [Range(-1, 1)]
        private float physicalResistance;

        [SerializeField]
        [Range(-1, 1)]
        private float fireResistance;

        public float MitigateDamage(float damage, EDamageType type)
        {
            throw new System.NotImplementedException();
        }
    }
}
