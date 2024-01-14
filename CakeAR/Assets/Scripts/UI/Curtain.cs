using Infrastructure.Services.InternetCheck;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private GameObject loadingText;
        [SerializeField] private GameObject errorMessage;
        [SerializeField] private TextMeshProUGUI errorMessageText;
        [SerializeField] private CanvasGroup curtain;
        [SerializeField] private Button restoreConnectionButton;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void HideCurtain() => 
            ChangeCurtainState(true);

        public void ShowCurtain() =>
            ChangeCurtainState(false);

        public void SetActiveLoadingText(bool isShowLoadingText) =>
            loadingText.SetActive(isShowLoadingText);

        public void SetActiveErrorMessage(bool isShowErrorMessage) =>
            errorMessage.SetActive(isShowErrorMessage);

        public void SetErrorMessage(string errorMessageOnScreen, bool buttonActiveStatus)
        {
            restoreConnectionButton.gameObject.SetActive(buttonActiveStatus);
            
            errorMessageText.text = errorMessageOnScreen;
        }

        public void InitRestoreConnectionButton(IInternetChecker internetChecker)
        {
            restoreConnectionButton.onClick.RemoveAllListeners();
            restoreConnectionButton.onClick.AddListener(internetChecker.TryToReconnect);
        }

        private void ChangeCurtainState(bool isTransparent) => 
            curtain.alpha = isTransparent ? 0 : 1;
    }
}