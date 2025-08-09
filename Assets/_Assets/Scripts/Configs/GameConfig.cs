using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName =  nameof(GameConfig), menuName = "Game/Configs/" +  nameof(GameConfig), order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int BotsCount = 10;
        public Vector2 BotSpawnLimits = new(19, 19);
        /// <summary>
        /// In seconds.
        /// </summary>
        public float RespawnDelay = 1f;
    }
}