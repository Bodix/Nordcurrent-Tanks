using Evolutex.Evolunity.Structs;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = nameof(AIConfig), menuName = "Game/Configs/" + nameof(AIConfig), order = 0)]
    public class AIConfig : ScriptableObject
    {
        public float MaxRotationDegrees = 90;
        public FloatRange RandomRotationInterval = new(2, 4);
    }
}