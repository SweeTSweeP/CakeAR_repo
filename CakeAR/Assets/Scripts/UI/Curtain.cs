using Infrastructure.States;
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
        [SerializeField] private TextMeshPro errorMessageText;
        [SerializeField] private CanvasGroup curtain;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void HideCurtain() => 
            ChangeCurtainState(true);

        public void ShowCurtain(bool isShowLoadingText) =>
            ChangeCurtainState(false);

        public void SetActiveLoadingText(bool isShowLoadingText) =>
            loadingText.SetActive(isShowLoadingText);

        private void ChangeCurtainState(bool isTransparent) => 
            curtain.alpha = isTransparent ? 0 : 1;
    }
}