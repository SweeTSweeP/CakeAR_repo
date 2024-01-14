using UnityEngine;

namespace Infrastructure.Services.GameObjectDisposer
{
    public interface IGameObjectDisposer : IService
    {
        void AddObjectToDispose(GameObject gameObjectToDispose);
        void Dispose();
    }
}