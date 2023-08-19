using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HOT.Skills
{
    [Serializable]
    public class Skill
    {
        [SerializeField] private SkillTargetType skillTargetType;
        [SerializeField] private int minDamage;
        [SerializeField] private int maxDamage;

        public SkillTargetType SkillTargetType => skillTargetType;

        public Skill(SkillTargetType skillTargetType, int minDamage, int maxDamage)
        {
            this.skillTargetType = skillTargetType;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
        }

        public int GetDamage() => Random.Range(minDamage, maxDamage + 1);
    }
}