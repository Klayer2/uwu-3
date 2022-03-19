using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReLost.PlayerNameSpace;
namespace ReLost.Utilities
{

    public class MouseInvisibleLocker : MonoBehaviour
    {
        private static CinemachineFreeLook vCam;
        private static WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
        private static CameraZoom cameraZoom = null;
        private static GameObject player = null;
        public static MonoBehaviour camMono;

        private void Start()
        {
            camMono = Camera.main.GetComponent<MonoBehaviour>();
            player = FindObjectOfType<Player>().gameObject;
            cameraZoom = player.GetComponent<CameraZoom>();
            vCam = FindObjectOfType<CinemachineFreeLook>();
        }

        private void OnApplicationQuit()
        {
            vCam.m_XAxis.m_InputAxisName = "Mouse X";
            vCam.m_YAxis.m_InputAxisName = "Mouse Y";
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraZoom.enabled = true;
        }

        public static IEnumerator LockDelayCoroutine()
        {
            Cursor.lockState = CursorLockMode.Locked;
            yield return waitForSeconds;
            Cursor.visible = false;
            vCam.m_XAxis.m_InputAxisName = "Mouse X";
            vCam.m_YAxis.m_InputAxisName = "Mouse Y";
            cameraZoom.enabled = true;
        }

        public static IEnumerator UnlockDelayCoroutine()
        {
            Cursor.visible = true;
            yield return waitForSeconds;
            Cursor.lockState = CursorLockMode.Confined;
            vCam.m_XAxis.m_InputAxisName = null;
            vCam.m_XAxis.m_InputAxisValue = 0f;
            vCam.m_YAxis.m_InputAxisName = null;
            vCam.m_YAxis.m_InputAxisValue = 0f;
            cameraZoom.enabled = false;
        }
        public void StartLock()
        {
            MouseInvisibleLocker.camMono.StartCoroutine(MouseInvisibleLocker.LockDelayCoroutine());
        }

        public void StartUnlock()
        {
            StartCoroutine(MouseInvisibleLocker.UnlockDelayCoroutine());
        }

        public void ChangeLock()
        {
            if (Cursor.lockState == CursorLockMode.Locked || Cursor.visible == false)
            {
                StartUnlock();
                return;
            }

            if (Cursor.lockState == CursorLockMode.Confined || Cursor.visible == true)
            {
                StartLock();
                return;
            }
        }
    }
}