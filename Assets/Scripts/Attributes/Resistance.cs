using UnityEngine;

namespace Scripts.Attributes
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Resistance", menuName = "Attributes/Resistance")]
    public class Resistance : ScriptableObject
    {
        [Header("Resistance Settings")]
        [Range(-1, 1)]
        public float physical;

        [Range(-1, 1)]
        public float fire;

        [Range(-1, 1)]
        public float ice;

        [Range(-1, 1)]
        public float lightning;

        [Range(-1, 1)]
        public float poison;

        [Range(-1, 1)]
        public float holy;

        [Range(-1, 1)]
        public float dark;

        public float ResistDamage(float damage, EDamageType type)
        {
            switch (type)
            {
                case EDamageType.Physical:
                    return damage * (1 - physical);
                case EDamageType.Fire:
                    return damage * (1 - fire);
                case EDamageType.Ice:
                    return damage * (1 - ice);
                case EDamageType.Lightning:
                    return damage * (1 - lightning);
                case EDamageType.Poison:
                    return damage * (1 - poison);
                case EDamageType.Holy:
                    return damage * (1 - holy);
                case EDamageType.Dark:
                    return damage * (1 - dark);
                default:
                    return damage;
            }
        }
    }
}
