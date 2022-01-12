using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.Tooltip
{
    public class GeneralTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string header;
        private static WaitForSeconds wait = new WaitForSeconds(0.5f);
        private IEnumerator co;
        [Multiline()]
        [SerializeField] private string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            co = TooltipShowDelay();
            StartCoroutine(co);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GeneralTooltipSystem.Hide();
            StopCoroutine(co);
        }

        private IEnumerator TooltipShowDelay()
        {
            yield return wait;
            GeneralTooltipSystem.Show(content, header);
            
        }
    }

}
