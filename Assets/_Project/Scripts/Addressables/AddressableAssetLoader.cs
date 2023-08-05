using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Addressable
{
    public static class AddressableAssetLoader
    {
        public async static Task<T> LoadAsset<T>(AssetReference asset)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(asset);
            GameObject loadedAsset = await handle.Task;

            return loadedAsset.GetComponent<T>();
        }
        
        public async static Task<T> LoadInstantiatableAsset<T>(AssetReference asset, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(asset, parent);
            GameObject loadedAsset = await handle.Task;

            return loadedAsset.GetComponent<T>();
        }

        public static void UnloadAsset(GameObject asset)
        {
            if (asset == null) return;
            
            asset.SetActive(false);
            Addressables.ReleaseInstance(asset);
        }
    }
}