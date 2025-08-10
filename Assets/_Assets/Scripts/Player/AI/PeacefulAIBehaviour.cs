using System.Collections;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(PeacefulAIBehaviour), menuName = "Game/AI Behaviours/" + nameof(PeacefulAIBehaviour), order = 0)]
    public class PeacefulAIBehaviour : AIBehaviour
    {
        protected override IEnumerator ShootingCoroutine()
        {
            // Do nothing.
            yield return null;
        }
    }
}