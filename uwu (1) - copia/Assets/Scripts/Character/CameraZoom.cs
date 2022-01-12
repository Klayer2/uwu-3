using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{   
    [SerializeField] private CinemachineFreeLook CameraToZoom;
    [SerializeField] private float zoomSpeed = 40f;
    [SerializeField] private float zoomAcceleration = 8f;
    [SerializeField] private float zoomInnerRange = 7f;
    [SerializeField] private float zoomOuterRange = 97.5f;
    [SerializeField] private float Camera_Acceleration = 3.5f;

    private float currentMiddleRadius = 18f;
    private float newMiddleRadius = 18f;

    [SerializeField] private float zoomYAxis = 0f;

    private void Awake()
    {
        CameraToZoom = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {   // introduces the data that the MSW is giving
        zoomYAxis = Input.GetAxisRaw("Mouse ScrollWheel");
        if(zoomYAxis != 0)
        {
            if(zoomYAxis > 0)
            {   
                newMiddleRadius = currentMiddleRadius - zoomSpeed;
                UpdateZoomLevel();
            }
            else
            {
                newMiddleRadius = currentMiddleRadius + zoomSpeed;
                UpdateZoomLevel();
            }
            CameraToZoom.m_XAxis.m_MaxSpeed = Camera_Acceleration;
        }
        
    }

    private void UpdateZoomLevel()
    {   
        // lerp calculates the zoom level and clamp sets the limits of the zoom distance
        if (currentMiddleRadius == newMiddleRadius) { return; }

        currentMiddleRadius = Mathf.Lerp(currentMiddleRadius, newMiddleRadius, zoomAcceleration * Time.deltaTime);
        currentMiddleRadius = Mathf.Clamp(currentMiddleRadius, zoomInnerRange, zoomOuterRange);

        CameraToZoom.m_Orbits[1].m_Radius = currentMiddleRadius;
        CameraToZoom.m_Orbits[0].m_Height = CameraToZoom.m_Orbits[1].m_Radius;
        CameraToZoom.m_Orbits[2].m_Height = -CameraToZoom.m_Orbits[1].m_Radius;

    }
}
