using UnityEngine;

namespace ReLost.Utilities
{
    public class ToogleActiveWithKeyPress : MonoBehaviour
    {
        [SerializeField] private GameObject objectToToggle = null;

        public void ActiveToggle()
        {

             objectToToggle.SetActive(!objectToToggle.activeSelf);
            
        }
    }
}

