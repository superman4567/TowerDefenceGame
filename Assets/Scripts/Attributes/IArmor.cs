using UnityEngine;

namespace Scripts.Attributes
{
    public interface IArmor
    {
        float MitigateDamage(float damage, EDamageType type);
    }
}
