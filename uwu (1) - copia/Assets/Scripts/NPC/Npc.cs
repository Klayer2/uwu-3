using ReLost.Events;
using ReLost.Interactables;
using ReLost.NPCs.Occupations;
using ReLost.Utilities;
using UnityEngine;

namespace ReLost.NPCs
{
    public class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField] private NpcEvent onStartInteraction = null;

        [SerializeField] private new string name = "New NPC Name";

        [SerializeField] private string greetingText = "Hello adventurer";

        [SerializeField] GameObject npcUI = null;

        public string Name => name;
        public string GreetingText => greetingText;
        public GameObject OtherInteractor { get; private set; } = null;
        public IOccupation[] Occupations { get; private set; } = new IOccupation[0];

        private void Awake() => Occupations = GetComponents<IOccupation>();

        public void Interact(GameObject other) 
        {
            OtherInteractor = other;

            onStartInteraction.Raise(this);
            npcUI.SetActive(true);
            StartCoroutine(MouseInvisibleLocker.UnlockDelayCoroutine());
        }

    }
}


