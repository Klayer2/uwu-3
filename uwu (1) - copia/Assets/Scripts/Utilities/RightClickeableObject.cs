using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightClickeableObject : MonoBehaviour, IPointerClickHandler
{
    //private GameObject thisObject = null;

    //private void Start()
    //{
    //    thisObject = this.gameObject;
    //}
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {


            Debug.Log("Right click");
        }
            
    }
}
