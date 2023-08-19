using HOT.Creature;

namespace HOT.Battle
{
    public class BattleSetup
    {
        public Humanoid[] Allies { get; }
        public Humanoid[] Enemies { get; }
        
        public BattleSetup(Humanoid[] allies, Humanoid[] enemies)
        {
            Allies = allies;
            Enemies = enemies;
        }
    }
}