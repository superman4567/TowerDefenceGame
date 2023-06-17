using UnityEngine;

namespace Scripts.Building
{
    public class StructureSettings : ScriptableObject
    {
        [Header("Structure Information")]
        public int value;

        public string structureName;

        [TextArea(3, 5)]
        public string description;
    }
}
