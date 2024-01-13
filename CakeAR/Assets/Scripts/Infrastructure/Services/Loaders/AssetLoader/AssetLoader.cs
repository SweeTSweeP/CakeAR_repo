using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GLTFast;
using UnityEngine;

namespace Infrastructure.Services.Loaders.AssetLoader
{
    public class AssetLoader : IAssetLoader
    {
        public async UniTask<bool> LoadModel(string url, string newModelName)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromSeconds(5));
            
            var gltf = new GltfImport();
            var success = await gltf.Load(url, cancellationToken: cts.Token);

            if (success) 
            {
                var gameObject = new GameObject(newModelName);
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