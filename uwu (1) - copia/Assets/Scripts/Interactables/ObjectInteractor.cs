using ReLost.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;

namespace ReLost.Interactables
{
    public class ObjectInteractor : MonoBehaviour
    {
        private IInteractable currentInteractable = null;
        public List<IInteractable> objectsToInteract;
        public List<Transform> objectsToInteractGameObject;
        public int availableObjects = 0;
        private ReferenceHolder referenceHolder;
        private GameObject inventoryInterface;

        private void Awake()
        {
            inventoryInterface = GameObject.Find("-----------UI-------------").transform.GetChild(0).gameObject;
            referenceHolder = FindObjectOfType<ReferenceHolder>();
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
                if(inventoryInterface.activeInHierarchy == true) { return; }
                if(objectsToInteract.Count >= 1)
                {

    #pragma warning disable CS0162
                    for(int i = 0; i < objectsToInteract.Count; i++)
    #pragma warning restore CS0162
                    {
                        var objectToInteract = objectsToInteract[i];
                        var objectToInteractTransform = objectsToInteractGameObject[i];
                        if (referenceHolder.pickUpDynamicInterface.transform.parent.gameObject.activeInHierarchy == false && objectToInteractTransform.gameObject.GetComponent<ItemPickup>() == true)
                        {
                            objectToInteract.Interact(transform.root.gameObject);
                            break;
                        }
                        else
                        {
                            objectToInteract.Interact(transform.root.gameObject);
                            referenceHolder.hoverPopUpInfo.HideInfo(); 
                        }
                        if (objectsToInteract.Count == 0)
                        {
                            objectsToInteract.Clear();
                            objectsToInteractGameObject.Clear();
                            availableObjects = objectsToInteract.Count;
                            break;
                        }
                        objectsToInteract.Remove(objectToInteract);
                        objectsToInteractGameObject.Remove(objectToInteractTransform);
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
            var interactablePickUp = interactableObject.gameObject.GetComponent<ItemPickup>();
            if (interactablePickUp == referenceHolder.pickUpDynamicInterface.itemPickUp)
            {
                referenceHolder.pickUpDynamicInterface.transform.parent.gameObject.SetActive(false);
            }

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

