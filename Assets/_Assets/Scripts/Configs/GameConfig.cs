using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Game/Configs/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int BotsCount = 3;
        public Vector2 BotSpawnLimits = new(19, 19);
        /// <summary>
        /// In seconds.
        /// </summary>
        public float RespawnDelay = 1f;
    }
}