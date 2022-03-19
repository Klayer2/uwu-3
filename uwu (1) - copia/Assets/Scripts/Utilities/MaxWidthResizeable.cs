using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Utilities
{

    public class MaxWidthResizeable : MonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElementFromHolder;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private Image image;
        [SerializeField] private float percentage = 1.552f;
        // Start is called before the first frame update
        private void OnEnable()
        {
            MaxWidthCheck();
        }

        private void MaxWidthCheck()
        {
            layoutElement.preferredWidth = 200;
            layoutElement.enabled = image.preferredWidth > (layoutElementFromHolder.preferredHeight / percentage) ? true : false;
            if(layoutElementFromHolder.preferredWidth < layoutElement.preferredWidth)
            {
                layoutElement.preferredWidth = 200; 
            }
            else
            {
                layoutElement.preferredWidth = (layoutElementFromHolder.preferredHeight / percentage);
            }
            
        }
    }

}

