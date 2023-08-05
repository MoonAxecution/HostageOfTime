using System.Threading.Tasks;
using HOT.Addressable;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Player
{
    public interface IPlayer {}
    
    public class PlayerCompositeRoot : MonoBehaviour, IPlayer, ITickable
    {
        [Inject] private Profile.Profile profile;
        [Inject] private TickerMono tickerMono;
        
        [Header("Assets")] 
        [SerializeField] private AssetReference playerLocationScreenAsset;
        
        [Header("Player Renderer Camera")]
        [SerializeField] private AssetReference playerRendererCameraAsset;
        [SerializeField] private Transform playerRendererCameraSpawnPoint;

        [Header("Components")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform playerCamera;

        private PlayerLocationScreen playerLocationScreen;
        private PlayerInput input;
        private PlayerLocomotion locomotion;
        private PlayerAnimation animation;

        private async void Awake()
        {
            this.Inject();

            profile.Equipment.WeaponEquiped += UpdateAnimationArmedState;
            
            input = new PlayerInput();
            locomotion = new PlayerLocomotion(characterController, playerTransform);
            
            animation = new PlayerAnimation(animator);
            UpdateAnimationArmedState();

            playerLocationScreen = await DependencyInjector.Resolve<UIManager>().OpenScreen<PlayerLocationScreen>(playerLocationScreenAsset);
            await LoadAsset<GameObject>(playerRendererCameraAsset, playerRendererCameraSpawnPoint);
            
            tickerMono.Add(this);
        }

        public void Tick(float deltaTime)
        {
            input.Update();

            float verticalInput = GetVerticalInput();
            float horizontalInput = GetHorizontalInput();
            float moveAmount = GetMoveAmount(verticalInput, horizontalInput);
            
            locomotion.Update(playerCamera, verticalInput, horizontalInput);
            animation.Update(moveAmount);
        }

        private float GetVerticalInput()
        {
            return Mathf.Clamp(input.VerticalInput + playerLocationScreen.VerticalInput, -1, 1);
        }
        
        private float GetHorizontalInput()
        {
            return Mathf.Clamp(input.HorizontalInput + playerLocationScreen.HorizontalInput, -1, 1);
        }

        private float GetMoveAmount(float verticalInput, float horizontalInput)
        {
            float moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            return moveAmount switch
            {
                > 0.5f => 1.0f,
                > 0 => 0.5f,
                _ => 0
            };
        }

        private void UpdateAnimationArmedState()
        {
            animation.UpdateArmedState(profile.Equipment.IsWeaponSet);
        }
        
        private async Task LoadAsset<T>(AssetReference asset, Transform parent) => await AddressableAssetLoader.LoadInstantiatableAsset<T>(asset, parent);

        private void OnDestroy()
        {
            profile.Equipment.WeaponEquiped -= UpdateAnimationArmedState;
            
            input.Dispose();
            tickerMono.Remove(this);
        }
    }
}