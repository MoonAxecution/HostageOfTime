using UnityEngine;

namespace HOT.Battle
{
    public class BattleSceneInitializer : SceneInitializer
    {
        [SerializeField] private BattleManager battleManager;

        protected override void OnAwaken()
        {
            battleManager.Init();
        }
    }
}