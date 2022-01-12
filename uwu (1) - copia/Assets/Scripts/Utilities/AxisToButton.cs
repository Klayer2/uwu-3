using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Utilities
{
    public class AxisToButton : MonoBehaviour
    {
        [SerializeField] private string axisButton;
        [SerializeField] private Button button;


        private void Update()
        {
            if (Input.GetButtonDown(axisButton))
            {
                button.onClick.Invoke();
            }
        }
    }

}
