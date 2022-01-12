using ReLost.NPCs.Occupations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ReLost.NPCs
{
    public class NpcUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI npcNameText = null;
        [SerializeField] private Transform occupationButtonHolder = null;
        [SerializeField] private GameObject occupationButtonPrefab = null;
        [SerializeField] private GameObject closeButtonPrefab = null;

        public void SetNpc(Npc npc)
        {
            npcNameText.text = npc.Name;

            foreach (Transform child in occupationButtonHolder)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < npc.Occupations.Length; i++)
            {
                GameObject buttonInstance = Instantiate(occupationButtonPrefab, occupationButtonHolder);
                GameObject buttonInstance2 = Instantiate(closeButtonPrefab, occupationButtonHolder);
                buttonInstance.GetComponent<OccupationButton>().Initialise(npc.Occupations[i]);
            }
        }
    }

}


