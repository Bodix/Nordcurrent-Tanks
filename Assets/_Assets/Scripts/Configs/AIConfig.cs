using Evolutex.Evolunity.Structs;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = nameof(AIConfig), menuName = "Game/Configs/" + nameof(AIConfig), order = 0)]
    public class AIConfig : ScriptableObject
    {
        public float RotationChangeDegrees = 90;
        public FloatRange RotationChangeRandomInterval = new(1, 2);
    }
}