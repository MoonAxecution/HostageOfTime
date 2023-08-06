using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Battle
{
    public class AllyBattleSide : BattleSide
    {
        [Inject] private Profile.Profile profile;
        [Inject] private UIManager uiManager;
        [Inject] private TickerMono tickerMono;

        [SerializeField] private TargetSelector targetSelector;
        [SerializeField] private AssetReference playerBattleScreenAsset;

        private Timer turnTimer;
        private BattleCreatureCompositeRoot selectedEnemy;
        private PlayerBattleScreen playerBattleScreen;

        public event Action TurnStarted;

        public async Task CreateSide()
        {
            this.Inject();
            
            targetSelector.TargetSelected += OnEnemySelected;
            
            CreateAllies();
            await CreateBattleScreen();
            
            CreateTurnTimer();
        }

        private void CreateTurnTimer()
        {
            turnTimer = new Timer(isStoped: true);
            turnTimer.TimerEnded += OnTurnTimerExpired;
            tickerMono.Add(turnTimer);
            
            playerBattleScreen.Init(turnTimer.LeftTime);
        }
        
        protected override void CreateAllies()
        {
            foreach (var ally in Allies)
            {
                ally.Init(profile.Equipment);
                ally.Died += OnAllyDied;
            }
        }

        public override void SetEnemies(List<BattleCreatureCompositeRoot> enemies)
        {
            base.SetEnemies(enemies);
            SelectDefaultEnemy();
        }

        private void SelectDefaultEnemy()
        {
            selectedEnemy = Enemies[0];
            targetSelector.SetDefaultSelectedTarget(selectedEnemy);
        }

        private async Task CreateBattleScreen()
        {
            playerBattleScreen = await uiManager.OpenScreen<PlayerBattleScreen>(playerBattleScreenAsset);
            playerBattleScreen.AttackPressed += StartAttack;
        }

        private void OnEnemySelected(ISelectable enemy)
        {
            selectedEnemy = enemy as BattleCreatureCompositeRoot;
        }

        public void ActivateTurn(int turnTime)
        {
            turnTimer.Reset(turnTime);
            ShowBattleScreen();
        }

        public override void StartAttack()
        {
            turnTimer.StopTimer();
            TurnStarted.Fire();
            base.StartAttack();
        }

        protected override void OnEnemyDied(BattleCreatureCompositeRoot enemy)
        {
            base.OnEnemyDied(enemy);
            
            if (Enemies.Count < 1) return;

            SelectDefaultEnemy();
        }

        protected override BattleCreatureCompositeRoot GetAttaackableEnemy() => selectedEnemy;

        public void HideBattleScreen()
        {
            playerBattleScreen.HideScreen();
        }
        
        private void ShowBattleScreen()
        {
            playerBattleScreen.ShowScreen();
        }

        private void OnTurnTimerExpired()
        {
            HideBattleScreen();
            InvokeTurnMadeEvent();
        }

        public void ClearSide()
        {
            uiManager.CloseScreen(playerBattleScreen);
            tickerMono.Remove(turnTimer);
        }
    }
}