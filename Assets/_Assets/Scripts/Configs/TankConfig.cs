using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Tank Config", menuName = "Game/Tank Config", order = 0)]
    public class TankConfig : ScriptableObject
    {
        /// <summary>
        /// In meters per second.
        /// </summary>
        public float MoveSpeed = 3f;
        /// <summary>
        /// In degrees per second.
        /// </summary>
        public float RotationSpeed = 180f;
        
        /// <summary>
        /// In meters per second.
        /// </summary>
        [Space]
        public float ShellSpeed = 20f;
        public Projectile ShellPrefab;
    }
}