using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI.PlayerInfoSection
{
    public class PlayerInfoSectionUI : MonoBehaviour
    {
        [SerializeField] private GameObject section;
        [SerializeField] private Button openSectionButton;

        private void Awake()
        {
            openSectionButton.onClick.AddListener(ShowSection);
        }

        private void ShowSection()
        {
            section.SetActive(true);
        }

        public void HideSection()
        {
            section.SetActive(false);
        }
    }
}