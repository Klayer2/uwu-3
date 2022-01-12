using UnityEngine;

namespace ReLost.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "GameEvents/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }

    }
}