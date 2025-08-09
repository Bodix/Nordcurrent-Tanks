using UnityEngine;

namespace Game
{
    // Use this CreateAssetMenu pattern for every new implementation of AIBehaviour.
    // [CreateAssetMenu(fileName = nameof(ClassName), menuName = "Game/AI Behaviours/" + nameof(ClassName), order = 0)]
    public abstract class AIBehaviour : ScriptableObject
    {
        public void HandleCollision(Collision collision)
        {
            
        }
    }
}