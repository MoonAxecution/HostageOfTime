using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI
{
    public class EquipmentCellView : MonoBehaviour
    {
        [SerializeField] private Image icon;

        public void InitIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            icon.enabled = icon.sprite != null;
        }
    }
}