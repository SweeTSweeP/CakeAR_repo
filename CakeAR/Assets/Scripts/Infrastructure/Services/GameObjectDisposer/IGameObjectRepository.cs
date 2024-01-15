using UnityEngine;

namespace Infrastructure.Services.GameObjectDisposer
{
    public interface IGameObjectRepository : IService
    {
        GameObject CurrentGameObject { get; }
        void AddObjectToDispose(GameObject gameObjectToRepository);
        void Dispose();
    }
}