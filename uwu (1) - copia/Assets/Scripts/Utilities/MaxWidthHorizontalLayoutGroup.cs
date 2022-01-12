using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Utilities
{
    public class MaxWidthHorizontalLayoutGroup : MonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElement = null;
        [SerializeField] private HorizontalLayoutGroup horizontalLayout = null;
        private void Start()
        {
            MaxWidthCheck();
        }

        private void MaxWidthCheck()
        {

            layoutElement.enabled = horizontalLayout.preferredWidth > layoutElement.preferredWidth ? true : false;

        }
    }

}

