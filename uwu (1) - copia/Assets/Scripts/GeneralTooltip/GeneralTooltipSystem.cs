using UnityEngine;

namespace ReLost.Tooltip
{
    public class GeneralTooltipSystem : MonoBehaviour
    {
        private static GeneralTooltipSystem current;

        [SerializeField] private GeneralTooltip tooltip;


        public void Awake()
        {
            current = this;
        }

        public static void Show(string content, string header = "")
        {
            current.tooltip.SetText(content, header);
            current.tooltip.gameObject.SetActive(true);
        }
        public static void Hide()
        {
            current.tooltip.gameObject.SetActive(false);
        }
    }
}

