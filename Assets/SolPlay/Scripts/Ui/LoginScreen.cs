using Frictionless;
using SolPlay.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace SolPlay.Scripts.Ui
{
    public class LoginScreen : MonoBehaviour
    {
        public Button PhantomLoginButton;
        public GameObject ConnectedRoot;
        public GameObject NotConnectedRoot;
        public GameObject TabBarRoot;

        private void Awake()
        {
            PhantomLoginButton.onClick.AddListener(OnPhantomButtonClicked);
           
        }

        private void Start()
        {
            UpdateContent();
        }

        private void OnRepositoryButtonClicked()
        {
            Application.OpenURL("https://github.com/Woody4618/SolanaUnityDeeplinkExample");
        }

        private void OnYoutubeButtonClicked()
        {
            Application.OpenURL("https://www.youtube.com/watch?v=mS5Fx_yzcHw&ab_channel=SolPlay");
        }

        private async void OnDevnetInGameWalletButtonClicked()
        {
            await ServiceFactory.Resolve<WalletHolderService>().Login(true);
            UpdateContent();
        }

        private async void OnPhantomButtonClicked()
        {
            await ServiceFactory.Resolve<WalletHolderService>().Login(false);
            UpdateContent();
        }

        private void UpdateContent()
        {
            bool isLoggedIn = ServiceFactory.Resolve<WalletHolderService>().IsLoggedIn;
            ConnectedRoot.gameObject.SetActive(isLoggedIn);
            NotConnectedRoot.gameObject.SetActive(!isLoggedIn);
            if (TabBarRoot != null)
            {
                TabBarRoot.gameObject.SetActive(isLoggedIn);
            }
        }
    }
}
