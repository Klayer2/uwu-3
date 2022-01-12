using ReLost.NPCs.Occupations;
using TMPro;
using UnityEngine;

namespace ReLost.NPCs
{
    public class NpcUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI npcNameText = null;
        [SerializeField] private TextMeshProUGUI npcGreetingText = null;
        [SerializeField] private Transform occupationButtonHolder = null;
        [SerializeField] private GameObject occupationButtonPrefab = null;
        [SerializeField] private GameObject closeButtonPrefab = null;

        public void SetNpc(Npc npc)
        {
            npcNameText.text = npc.Name;
            npcGreetingText.text = npc.GreetingText;

            foreach (Transform child in occupationButtonHolder)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < npc.Occupations.Length; i++)
            {
                GameObject buttonInstance = Instantiate(occupationButtonPrefab, occupationButtonHolder);
                buttonInstance.GetComponent<OccupationButton>().Initialise(npc.Occupations[i], npc.OtherInteractor);
            }
            GameObject buttonInstance2 = Instantiate(closeButtonPrefab, occupationButtonHolder);
        }
    }

}


