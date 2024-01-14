using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ARSetup
{
    public class ARSetup : MonoBehaviour
    {
        [SerializeField] private GameObject trackedPrefab;
        [SerializeField] private ARTrackedImageManager _arTrackedImageManager;
        [SerializeField] private XRReferenceImageLibrary xrReferenceImageLibrary;
        [SerializeField] private Sprite image;

        private void Start()
        {
            ARSession.stateChanged += ARSessionOnStateChanged; 
        }

        private void ARSessionOnStateChanged(ARSessionStateChangedEventArgs obj)
        {
            if (!(ARSession.state == ARSessionState.SessionInitializing ||
                  ARSession.state == ARSessionState.SessionTracking))
                return;
            
            var library = _arTrackedImageManager.CreateRuntimeLibrary();
            if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                var croppedTexture = ConvertSpriteToCroppedTexture();

                mutableLibrary.ScheduleAddImageWithValidationJob(
                    croppedTexture,
                    "CakeMarker", 
                    0.1f);
            }
        }

        private Texture2D ConvertSpriteToCroppedTexture()
        {
            var croppedTexture = new Texture2D( (int)image.rect.width, (int)image.rect.height );
            var pixels = image.texture.GetPixels(  (int)image.textureRect.x, 
                (int)image.textureRect.y, 
                (int)image.textureRect.width, 
                (int)image.textureRect.height );
            croppedTexture.SetPixels( pixels );
            croppedTexture.Apply();
            return croppedTexture;
        }
    }
}
