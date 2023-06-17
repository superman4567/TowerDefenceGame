using UnityEngine;

namespace Scripts.Cards
{
    public delegate void CardLayoutEvent(CardHandLayout layout);

    [System.Serializable]
    public struct CardLayout
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    [CreateAssetMenu(fileName = "Card Layout", menuName = "Card Layout")]
    public class CardHandLayout : ScriptableObject
    {
        [Header("Card Layout")]
        public CardLayout[] layout;
        public CardLayoutEvent OnLayoutChanged;

        private void OnValidate()
        {
            OnLayoutChanged?.Invoke(this);
        }

        public (Vector3 position, Vector3 rotation) GetLayout(int index)
        {
            return (layout[index].position, layout[index].rotation);
        }
    }
}
