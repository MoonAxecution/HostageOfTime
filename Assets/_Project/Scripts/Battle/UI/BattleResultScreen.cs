using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Screen = HOT.UI.Screen;

namespace HOT.Battle.UI
{
    public class BattleResultScreen : Screen
    {
        [SerializeField] private TMP_Text headerLabel;
        [SerializeField] private Button closeScreen;

        protected override void OnAwaken()
        {
            base.OnAwaken();
            
            closeScreen.onClick.AddListener(CloseScreen);
        }

        public void Init(string header)
        {
            headerLabel.text = header;
        }
    }
}