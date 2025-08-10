using System.Collections;
using Evolutex.Evolunity.Structs;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(AggressiveAIBehaviour), menuName = "Game/AI Behaviours/" + nameof(AggressiveAIBehaviour), order = 0)]
    public class AggressiveAIBehaviour : AIBehaviour
    {
        public FloatRange ShootRandomInterval = new(1f, 2f);
        
        protected override IEnumerator ShootingCoroutine()
        {
            while (IsActive)
            {
                yield return new WaitForSeconds(ShootRandomInterval.RandomWithin);
                
                Tank.Shoot();
            }
        }
    }
}