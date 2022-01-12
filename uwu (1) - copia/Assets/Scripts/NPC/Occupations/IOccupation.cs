using UnityEngine;

namespace ReLost.NPCs.Occupations
{

    public interface IOccupation
    {

        string Name { get; }
        void Trigger(GameObject other);

    }

}

