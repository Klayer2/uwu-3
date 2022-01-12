using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCanvasFind : MonoBehaviour
{
    [SerializeField] private GameObject NpcCanvas = null;

    public void Start()
    {
        FindCanvas();
    }

    public void FindCanvas()
    {
        NpcCanvas = this.gameObject.transform.parent.parent.parent.gameObject;
    }

    public void DisableCanvas()
    {
        NpcCanvas.SetActive(false);
    }
}
