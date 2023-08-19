using System;
using UnityEngine;
using UnityEngine.UI;

namespace HOT.Skills
{
    public class SkillButtonView : MonoBehaviour
    {
        [SerializeField] private Button skillButton;

        private Skill skill;

        public event Action<Skill> Pressed;

        private void Awake()
        {
            skillButton.onClick.AddListener(OnSkillButtonPressed);
        }

        public void Init(Skill skill)
        {
            this.skill = skill;
        }

        private void OnSkillButtonPressed()
        {
            Pressed.Fire(skill);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}