using System.Collections.Generic;
using UnityEngine;

namespace ReLost.Interactables
{
    public class NpcInteractor : MonoBehaviour
    {
        private IInteractable currentInteractable = null;
        [SerializeField]private GameObject NormalVendorCanvas = null;
        [SerializeField]private GameObject NormalNpcCanvas = null;
        [SerializeField]private ObjectInteractor objectInteractor;
        private List<IInteractable> npcToInteract;

        private void Awake()
        {
            npcToInteract = new List<IInteractable>();
        }

        private void Update()
        {
            CheckForInteraction();
        }

        private void CheckForInteraction()
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (npcToInteract.Count > 0 && !(objectInteractor.availableObjects > 0))
                {
                    for (int i = npcToInteract.Count - 1; i < npcToInteract.Count; i++)
                    {
                        npcToInteract[i].Interact(transform.root.gameObject);
                    }
                }
                else { return; }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.CompareTag("NormalNpc")) && !(other.CompareTag("VendorNpc")))
            {
                return;
            }

            var interactable = other.GetComponent<IInteractable>();

            if (interactable == null) { return; }

            npcToInteract.Add(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!(other.CompareTag("NormalNpc")) && !(other.CompareTag("VendorNpc")))
            {
                return;
            }
            var interactable = other.GetComponent<IInteractable>();

            if ( (other.gameObject.CompareTag("NormalNpc") == true) && (NormalNpcCanvas.activeInHierarchy == true))
            {
                NormalNpcCanvas.SetActive(false);
            }
            if ((other.gameObject.CompareTag("VendorNpc") == true) && (NormalNpcCanvas.activeInHierarchy == true || NormalVendorCanvas.activeInHierarchy == true))
            {
                NormalNpcCanvas.SetActive(false);
                NormalVendorCanvas.SetActive(false);
            }
            if (interactable == null)
            {
                npcToInteract.Remove(interactable);
                return;
            }
            if (interactable != currentInteractable)
            {
                npcToInteract.Remove(interactable);
                return;
            }
        }
    }
}

