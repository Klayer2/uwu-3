using UnityEngine;
using Cinemachine;

public class PlayerFollow : MonoBehaviour
{
    private CinemachineFreeLook vCam;
    private Transform tPlayer;
    [SerializeField] private GameObject inventoryCanvas;

    private void Awake()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        if (tPlayer == null)
        {
            FindPlayer();
        }

    }

    private void FindPlayer()
    {
        
            tPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            vCam.m_Follow = tPlayer.transform;
            vCam.m_LookAt = tPlayer.transform;

    }
}
