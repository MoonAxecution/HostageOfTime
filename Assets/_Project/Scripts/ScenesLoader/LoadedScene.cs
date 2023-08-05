using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace HOT
{
    public struct LoadedScene
    {
        public AsyncOperationHandle<SceneInstance> Scene { get; }
        public bool IsAdditive { get; private set; }
        
        public string SceneName => Scene.IsValid() ? Scene.Result.Scene.name : string.Empty;

        public LoadedScene(AsyncOperationHandle<SceneInstance> scene, bool isAdditive)
        {
            Scene = scene;
            IsAdditive = isAdditive;
        }
    }
}