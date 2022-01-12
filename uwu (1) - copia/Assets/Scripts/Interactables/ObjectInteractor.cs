using System.Collections.Generic;
using UnityEngine;

namespace ReLost.Interactables
{
    public class ObjectInteractor : MonoBehaviour
    {
        private IInteractable currentInteractable = null;
        private List<IInteractable> objectsToInteract;
        private List<Transform> objectsToInteractGameObject;
        public int availableObjects = 0;

        private void Awake()
        {
            objectsToInteract = new List<IInteractable>();
            objectsToInteractGameObject = new List<Transform>();
        }

        private void LateUpdate()
        {
            CheckForInteraction();
        }

        private void CheckForInteraction()
        {
            if (Input.GetButtonDown("Interact"))
            {
                if(objectsToInteract.Count >= 1)
                {

    #pragma warning disable CS0162
                    for(int i = objectsToInteract.Count - 1; i < objectsToInteract.Count; i--)
    #pragma warning restore CS0162
                    {
                        objectsToInteract[i].Interact(transform.root.gameObject);
                        objectsToInteract.RemoveAt(i);
                        objectsToInteractGameObject.RemoveAt(i);
                        availableObjects = objectsToInteract.Count;
                        break;
                        
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.CompareTag("InteractableObject")))
            {
                return;
            }

            var interactable = other.GetComponent<IInteractable>();
            var interactableObject = other.GetComponent<Transform>();

            if (interactable == null) { return; }

            objectsToInteract.Add(interactable);
            objectsToInteractGameObject.Add(interactableObject);
            availableObjects = objectsToInteract.Count;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!(other.CompareTag("InteractableObject")))
            {
                return;
            }

            var interactable = other.GetComponent<IInteractable>();
            var interactableObject = other.GetComponent<Transform>();


            if (interactable == null)
            {
                objectsToInteract.Remove(interactable);
                objectsToInteractGameObject.Remove(interactableObject);
                availableObjects = objectsToInteract.Count;
                return;
            }

            if (interactable != currentInteractable)
            {
                objectsToInteract.Remove(interactable);
                objectsToInteractGameObject.Remove(interactableObject);
                availableObjects = objectsToInteract.Count;
                return;
            }

        }
    }
}

