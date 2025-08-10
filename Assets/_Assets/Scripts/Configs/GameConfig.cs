using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName =  nameof(GameConfig), menuName = "Game/Configs/" +  nameof(GameConfig), order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int BotsCount = 10;
        public Vector2 BotSpawnLimits = new(18, 18);
        /// <summary>
        /// In seconds.
        /// </summary>
        public float RespawnDelay = 1f;
    }
}