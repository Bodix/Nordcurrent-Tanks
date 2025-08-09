using Evolutex.Evolunity.Extensions;
using UnityEngine;

namespace Game
{
    public class AIBot : AbstractPlayer
    {
        protected override Pose GetRespawnPose()
        {
            return new Pose(
                Vector3.zero.Randomize(
                    (-GameConfig.BotSpawnLimits.x / 2, GameConfig.BotSpawnLimits.x / 2),
                    (0, 0),
                    (-GameConfig.BotSpawnLimits.y / 2, GameConfig.BotSpawnLimits.y / 2)),
                Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}