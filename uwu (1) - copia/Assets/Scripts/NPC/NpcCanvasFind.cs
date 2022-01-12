using UnityEngine;

public class NpcCanvasFind : MonoBehaviour
{
    [SerializeField] private GameObject npcCanvas = null;

    public GameObject NpcCanvas => npcCanvas;

    private void Awake()
    {

        FindCanvas();
    }

    public void FindCanvas()
    {
        if(NpcCanvas != null) { return; }
        npcCanvas = GameObject.FindGameObjectWithTag("NpcCanvas");
    }

    public void DisableCanvas()
    {
        NpcCanvas.SetActive(false);
    }
    public void EnableCanvas()
    {
        NpcCanvas.SetActive(true);
    }
}
