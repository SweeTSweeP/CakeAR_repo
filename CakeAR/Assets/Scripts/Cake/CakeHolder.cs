using Infrastructure.Services;
using Infrastructure.Services.GameObjectDisposer;
using UnityEngine;

namespace Cake
{
    public class CakeHolder : MonoBehaviour
    {
        [SerializeField] private GameObject pointer;
        
        private void Awake() => ShowCake();

        private void OnEnable() => ShowCake();

        private void OnDisable() => HideCake();

        private void OnDestroy() => HideCake();


        private void ShowCake()
        {
            var gameObjectRepository = AllServices.Container.Single<IGameObjectRepository>();

            if (VerifyCake(gameObjectRepository)) return;

            gameObjectRepository.CurrentGameObject.SetActive(true);

            var holderTransform = pointer.transform;
            gameObjectRepository.CurrentGameObject.transform.position = holderTransform.position;
            gameObjectRepository.CurrentGameObject.transform.rotation = holderTransform.rotation;
            gameObjectRepository.CurrentGameObject.transform.localScale = holderTransform.localScale;
        }

        private bool VerifyCake(IGameObjectRepository gameObjectRepository)
        {
            if (gameObjectRepository.CurrentGameObject == null)
            {
                Debug.Log("Cake holder was not instantiated");
                return true;
            }

            if (gameObjectRepository.CurrentGameObject.transform.childCount == 0)
            {
                Debug.Log("Cake  was not instantiated");
                return true;
            }

            for (var i = 0; i < gameObjectRepository.CurrentGameObject.transform.childCount; i++)
            {
                var child = gameObjectRepository.CurrentGameObject.transform.GetChild(i);
                        
                Debug.Log($"child #{i} name: {child.name}");
                
                Debug.Log($"cake has {child.childCount} child count");
            }

            return false;
        }

        private void HideCake()
        {
            var gameObjectRepository = AllServices.Container.Single<IGameObjectRepository>();

            if (VerifyCake(gameObjectRepository)) return;

            gameObjectRepository.CurrentGameObject.SetActive(false);
        }
    }
}
