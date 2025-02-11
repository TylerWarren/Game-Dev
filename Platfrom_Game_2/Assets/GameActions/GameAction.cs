using UnityEngine;
using UnityEngine.Events;

namespace GameActions
{
    [CreateAssetMenu(menuName = "Actions/Game Action")]
    public class GameAction: ScriptableObject
    {
        public UnityAction<object> Raise { get; set; } = delegate { }; // Prevents NullReference
        public UnityAction RaiseNoArgs { get; set; } = delegate { }; // Prevents NullReference

        public void RaiseAction()
        {
            Debug.Log("GameAction: Raising No Args Event.");
            RaiseNoArgs?.Invoke();
        }

        public void RaiseAction<T>(T obj)
        {
            if (Raise != null)
            {
                Debug.Log($"GameAction: Raising Event with {obj}");
                Raise.Invoke(obj);
            }
            else
            {
                Debug.LogError("GameAction RaiseAction<T> was called but has no listeners!");
            }
        }
    }
}