using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Game/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int BotsCount = 3;
    }
}