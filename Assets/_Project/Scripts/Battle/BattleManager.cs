using System;
using System.Threading.Tasks;
using HOT.Battle.UI;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [Inject] private UIManager uiManager;

        [SerializeField] private int playerTurnTime;
        [SerializeField] private AllyBattleSide playerSide;
        [SerializeField] private EnemyBattleSide enemySide;

        [Header("Scenes")]
        [SerializeField] private AssetReference lobbySceneAsset;
        
        [Header("Screens")]
        [SerializeField] private AssetReference battleScreenAsset;
        [SerializeField] private AssetReference battleResultScreenAsset;

        private BattleScreen battleScreen;
        private BattleResultScreen battleResultScreen;

        public event Action Won;
        public event Action Lost;

        public async Task Init()
        {
            this.Inject();

            await CreateBattleScreen();
            await CreateSides();
            
            SetSideEnemies();
            SetPlayerTurn();
        }

        private async Task CreateBattleScreen()
        {
            battleScreen = await uiManager.OpenScreen<BattleScreen>(battleScreenAsset);
            battleScreen.Surrendered += OnPlayerSurrendered;
        }

        private void OnPlayerSurrendered()
        {
            EndBattle(false);
        }

        private async Task CreateSides()
        {
            await CreatePlayerSide();
            CreateEnemySide();
        }

        private void SetSideEnemies()
        {
            playerSide.SetEnemies(enemySide.Allies);
            enemySide.SetEnemies(playerSide.Allies);
        }
        
        private async Task CreatePlayerSide()
        {
            await playerSide.CreateSide();
            playerSide.TurnStarted += OnPlayerSideTurnStarted;
            playerSide.TurnMade += OnPlayerSideTurnMade;
        }

        private void CreateEnemySide()
        {
            enemySide.CreateSide();
            enemySide.TurnMade += OnEnemySideTurnMade;
        }

        private void OnPlayerSideTurnStarted()
        {
            playerSide.HideBattleScreen();
        }

        private void OnPlayerSideTurnMade()
        {
            if (enemySide.Allies.Count < 1)
            {
                EndBattle(true);
                return;
            }

            SetEnemyTurn();
        }

        private void OnEnemySideTurnMade()
        {
            if (playerSide.Allies.Count >= 1)
            {
                SetPlayerTurn();
                return;
            }
            
            EndBattle(false);
        }

        private void SetPlayerTurn()
        {
            playerSide.ActivateTurn(DateTime.Now.ToUnixTimestamp() + playerTurnTime);
        }

        private void SetEnemyTurn()
        {
            enemySide.StartAttack();
        }

        private void EndBattle(bool isWin)
        {
            playerSide.ClearSide();
            ShowResultScreen(isWin);
        }

        private async void ShowResultScreen(bool isWin)
        {
            battleScreen.DisableScreenInput();
            
            if (isWin)
                Won.Fire();
            else
                Lost.Fire();
            
            battleResultScreen = await DependencyInjector.Resolve<UIManager>().OpenScreen<BattleResultScreen>(battleResultScreenAsset);
            battleResultScreen.Init(isWin ? "You win!" : "You loose!");
            battleResultScreen.Closed += LoadLobby;
        }

        private void LoadLobby()
        {
            var scenesLoader = DependencyInjector.Resolve<ScenesLoader>();
            
            if (scenesLoader.IsCurrentSceneAdditive)
                scenesLoader.LoadPreviousScene();
            else
                scenesLoader.LoadSceneWithLoadingScreen(lobbySceneAsset);
        }
    }
}