using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Utilities
{
    public class MaxWidthText : MonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElement = null;
        [SerializeField] private TextMeshProUGUI text = null;
        private void Start()
        {
            MaxWidthCheck();
        }

        private void MaxWidthCheck()
        {

            layoutElement.enabled = text.preferredWidth > layoutElement.preferredWidth ? true : false;

        }
    }

}

