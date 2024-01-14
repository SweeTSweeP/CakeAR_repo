using Cysharp.Threading.Tasks;
using Infrastructure.Services.GameObjectDisposer;

namespace Infrastructure.Services.Loaders.AssetLoader
{
    public interface IAssetLoader : IService
    {
        UniTask<bool> LoadModel(string url, string newModelName, IGameObjectDisposer gameObjectDisposer);
    }
}