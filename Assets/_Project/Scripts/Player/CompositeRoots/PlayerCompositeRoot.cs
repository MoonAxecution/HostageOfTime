using System.Threading.Tasks;
using HOT.Addressable;
using HOT.Equipment;
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
        [SerializeField] private ObjectIdentifier objectIdentifier;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private EquipmentView equipmentView;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform playerCamera;

        private PlayerLocationScreen playerLocationScreen;
        private PlayerInput input;
        private PlayerLocomotion locomotion;
        private PlayerAnimation animation;
        
        private IActable currentActableObject;

        public ObjectIdentifier ObjectIdentifier => objectIdentifier;

        private async void Awake()
        {
            this.Inject();

            animator.keepAnimatorStateOnDisable = true;
            
            await InitComponents();

            tickerMono.Add(this);
        }

        private async Task InitComponents()
        {
            input = new PlayerInput();
            locomotion = new PlayerLocomotion(characterController, playerTransform);
            animation = new PlayerAnimation(animator, profile.Equipment.IsWeaponSet);

            equipmentView.Init(profile.Equipment.GetCell(EquipmentType.Weapon), 
                profile.Equipment.GetCell(EquipmentType.Helmet));

            objectIdentifier.ActableObjectFound += OnActableObjectFound;
            objectIdentifier.ActableObjectLost += OnActableObjectLost;

            playerLocationScreen = await DependencyInjector.Resolve<UIManager>().OpenScreen<PlayerLocationScreen>(playerLocationScreenAsset);
            playerLocationScreen.Acted += Act;
            
            await AddressableAssetLoader.LoadAsset<GameObject>(playerRendererCameraAsset, playerRendererCameraSpawnPoint);
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
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

        private void Act()
        {
            currentActableObject.Act();
        }

        private void OnActableObjectFound(IActable actableObject)
        {
            currentActableObject = actableObject;
            playerLocationScreen.ShowActButton();
        }

        private void OnActableObjectLost()
        {
            currentActableObject = null;
            playerLocationScreen.HideActButton();
        }

        private void OnDestroy()
        {
            input.Dispose();
            animation.Dispose();
            tickerMono.Remove(this);
        }

        public void SetPlayerCamera(Transform newCameraRef)
        {
            playerCamera = newCameraRef;
        }
    }
}