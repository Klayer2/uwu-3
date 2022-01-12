using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Utilities
{

    public class MaxWidth : MonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElement = null;
        [SerializeField] private Image image = null;
        // Start is called before the first frame update
        private void Start()
        {
            MaxWidthCheck();
        }

        private void MaxWidthCheck()
        {
            layoutElement.enabled = image.preferredWidth > layoutElement.preferredWidth ? true : false;
        }
    }

}

