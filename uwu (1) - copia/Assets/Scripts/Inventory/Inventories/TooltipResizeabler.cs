using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.GeneralTooltip
{
    public class TooltipResizeabler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerField = null;

        [SerializeField] private TextMeshProUGUI descriptionField = null;

        [SerializeField] private TextMeshProUGUI contentField = null;

        [SerializeField] private Image buffsField = null;

        [SerializeField] private LayoutElement layoutElement = null;

        private void OnEnable()
        {
            layoutElement.enabled = Math.Max(headerField.preferredWidth, contentField.preferredWidth) > layoutElement.preferredWidth;
            layoutElement.enabled = Math.Max(headerField.preferredWidth, descriptionField.preferredWidth) > layoutElement.preferredWidth;
            layoutElement.enabled = Math.Max(contentField.preferredWidth, descriptionField.preferredWidth) > layoutElement.preferredWidth;
        }
    }
}

