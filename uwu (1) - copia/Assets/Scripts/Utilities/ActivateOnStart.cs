using UnityEngine;

namespace ReLost.Utilities
{
    public class ActivateOnStart : MonoBehaviour
    {
        private void Start()
        {
            var camMono = Camera.main.GetComponent<MonoBehaviour>();
            camMono.StartCoroutine(MouseInvisibleLocker.LockDelayCoroutine());
        }

        
    }
}


