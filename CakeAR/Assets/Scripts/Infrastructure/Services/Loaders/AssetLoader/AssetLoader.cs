using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GLTFast;
using Infrastructure.Services.GameObjectDisposer;
using UnityEngine;

namespace Infrastructure.Services.Loaders.AssetLoader
{
    public class AssetLoader : IAssetLoader
    {
        public async UniTask<bool> LoadModel(string url, string newModelName, IGameObjectDisposer gameObjectDisposer)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromSeconds(5));
            
            var gltf = new GltfImport();
            var success = await gltf.Load(url, cancellationToken: cts.Token);

            if (success) 
            {
                var gameObject = new GameObject(newModelName);
                gameObjectDisposer.AddObjectToDispose(gameObject);
                
                await gltf.InstantiateMainSceneAsync(gameObject.transform, cts.Token);
            }
            else 
            {
                Debug.Log($"Loading glTF: {newModelName} failed!");
            }

            return success;
        }
    }
}