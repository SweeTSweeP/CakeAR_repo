using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.GameObjectDisposer
{
    public class GameObjectDisposer : IGameObjectDisposer
    {
        private readonly List<GameObject> _objectsToDispose = new();

        public void AddObjectToDispose(GameObject gameObjectToDispose) => 
            _objectsToDispose.Add(gameObjectToDispose);

        public void Dispose()
        {
            foreach (var objectDispose in _objectsToDispose) 
                Object.Destroy(objectDispose);
            
            _objectsToDispose.Clear();
        }
    }
}