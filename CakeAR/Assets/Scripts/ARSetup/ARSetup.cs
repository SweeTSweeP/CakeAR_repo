using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ARSetup
{
    public class ARSetup : MonoBehaviour
    {
        [SerializeField] private GameObject trackedPrefab;
        [SerializeField] private GameObject xrOrigin;
        [SerializeField] private GameObject arSession;

        private Texture2D _downloadedSprite;

        private void Start() => 
            SetupAR();

        private async void SetupAR()
        {
            var www = UnityWebRequestTexture.GetTexture(Constants.MarkerUrl);
            
            var task = www.SendWebRequest();
            
            await UniTask.WaitWhile(() => !task.isDone);

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                _downloadedSprite = ((DownloadHandlerTexture)www.downloadHandler).texture;

            xrOrigin.SetActive(true);
            arSession.SetActive(true);
            
            await UniTask.WaitWhile(() => !(ARSession.state == ARSessionState.SessionInitializing ||
                                      ARSession.state == ARSessionState.SessionTracking));

            var arTrackedImageManager = xrOrigin.AddComponent<ARTrackedImageManager>();
            var library = arTrackedImageManager.CreateRuntimeLibrary();
            
            if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                if (_downloadedSprite == null) return;
                
                var croppedTexture = _downloadedSprite ;

                mutableLibrary.ScheduleAddImageWithValidationJob(
                    croppedTexture,
                    "CakeMarker", 
                    0.1f);
            }

            arTrackedImageManager.requestedMaxNumberOfMovingImages = 5;
            arTrackedImageManager.trackedImagePrefab = trackedPrefab;

            arTrackedImageManager.enabled = true;
        }
    }
}
