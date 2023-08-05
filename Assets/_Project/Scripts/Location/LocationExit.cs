using HOT.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Location
{
    public class LocationExit : MonoBehaviour
    {
        [SerializeField] private AssetReference lobbySceneReference;
        
        private void OnTriggerEnter(Collider other)
        {
            var component = other.GetComponent<IPlayer>();
            
            if (component == null) return;
            
            DependencyInjector.Resolve<ScenesLoader>().LoadSceneWithLoadingScreen(lobbySceneReference);
        }
    }
}