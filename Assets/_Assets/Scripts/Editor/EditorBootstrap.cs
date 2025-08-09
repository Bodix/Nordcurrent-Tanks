using Evolutex.Evolunity.Editor.Utilities;
using UnityEditor;

namespace Game.Editor
{
    [InitializeOnLoad]
    public static class EditorBootstrap
    {
        static EditorBootstrap()
        {
            AutoExpandHierarchy.GameObjectsToExpand.Add("Logic");
            AutoExpandHierarchy.GameObjectsToExpand.Add("Game");
            AutoExpandHierarchy.GameObjectsToExpand.Add("Game/Bots");
        }
    }
}