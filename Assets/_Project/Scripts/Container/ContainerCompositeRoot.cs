using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.Container
{
    public class ContainerCompositeRoot : MonoBehaviour
    {
        [SerializeField] private ItemSettings[] items;
        [SerializeField] private ContainerView view;
    }
}