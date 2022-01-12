using UnityEngine;

namespace ReLost.Magic
{

    [CreateAssetMenu(fileName = "New Element", menuName = "Magic/Element")]
    public class Element : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}
