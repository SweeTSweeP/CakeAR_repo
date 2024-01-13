using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.Loaders.AssetLoader
{
    public interface IAssetLoader : IService
    {
        UniTask<bool> LoadModel(string url, string newModelName);
    }
}