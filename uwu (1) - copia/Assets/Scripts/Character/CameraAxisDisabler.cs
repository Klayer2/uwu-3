using Cinemachine;
using UnityEngine;

namespace ReLost.Utilities
{

    public class CameraAxisDisabler : MonoBehaviour
    {
        private bool isApplicationQuitting = false;


        private void OnEnable()
        {
            StartCoroutine(MouseInvisibleLocker.UnlockDelayCoroutine());
        }

        private void OnDisable()
        {
            if (isApplicationQuitting) { return; }
            var camMono = Camera.main.GetComponent<MonoBehaviour>();
            camMono.StartCoroutine(MouseInvisibleLocker.LockDelayCoroutine());
        }

        private void OnApplicationQuit()
        {
            isApplicationQuitting = true;
        }
    }

}


