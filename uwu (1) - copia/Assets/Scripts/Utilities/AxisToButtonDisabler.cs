using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReLost.Utilities
{
    public class AxisToButtonDisabler : MonoBehaviour
    {
        public AxisToButton[] axisToButtons;

        public void IsAxisToButtonEnabled(bool axisEnabled)
        {
            axisToButtons = GameObject.FindObjectsOfType<AxisToButton>();

            for (int i = 0; i < axisToButtons.Length; i++)
            {
                axisToButtons[i].enabled = axisEnabled;
            }
        }

    }

}
