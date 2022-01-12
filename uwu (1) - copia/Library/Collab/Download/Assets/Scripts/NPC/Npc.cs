using ReLost.Events;
using ReLost.Interactables;
using ReLost.NPCs.Occupations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReLost.NPCs
{
    public class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField] private NpcEvent onStartInteraction = null;

        [SerializeField] private new string name = "New NPC Name";

        [SerializeField] private string greetingText = "Hello adventurer";

        [SerializeField] GameObject npcUI = null;

        private IOccupation[] occupations = new IOccupation[0];

        public string Name => name;

        public IOccupation[] Occupations => occupations;

        private void Start() => occupations = GetComponents<IOccupation>();

        public void Interact(GameObject other) { onStartInteraction.Raise(this); npcUI.SetActive(true); }

    }
}


